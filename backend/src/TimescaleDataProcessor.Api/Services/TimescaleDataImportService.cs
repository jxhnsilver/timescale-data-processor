using Microsoft.EntityFrameworkCore;
using TimescaleDataProcessor.Api.Data.Context;
using TimescaleDataProcessor.Api.Entities;
using TimescaleDataProcessor.Api.Parsers;
using TimescaleDataProcessor.Api.Validators;

namespace TimescaleDataProcessor.Api.Services
{
    public class TimescaleDataImportService : ITimescaleDataImportService
    {
        private readonly IParserFactory _parserFactory;
        private readonly IRecordValidator _validator;
        private readonly IResultCalculator _calculator;
        private readonly ApplicationDbContext _dbContext;

        private const int MaxAllowedRows = 10000;
        private const int MinAllowedRows = 1;
        private const int BatchSize = 1000;

        public TimescaleDataImportService(
            IParserFactory parserFactory,
            IRecordValidator validator,
            IResultCalculator calculator,
            ApplicationDbContext dbContext)
        {
            _parserFactory = parserFactory;
            _validator = validator;
            _calculator = calculator;
            _dbContext = dbContext;
        }

        public async Task ProcessAsync(Stream stream, string fileName, CancellationToken ct)
        {
            var parser = _parserFactory.CreateParser(Path.GetExtension(fileName));

            var indicators = new List<double>(MaxAllowedRows);
            double sumExecutionTime = 0;
            var minStartTime = DateTime.MaxValue;
            var maxStartTime = DateTime.MinValue;

            var batch = new List<ValueRecord>(MaxAllowedRows);
            var totalRows = 0;

            using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
            try
            {
                await _dbContext.Values
                    .Where(v => v.FileName == fileName)
                    .ExecuteDeleteAsync(ct);

                await _dbContext.Results
                    .Where(r => r.FileName == fileName)
                    .ExecuteDeleteAsync(ct);

                await foreach (var parsedRecord in parser.ParseAsync(stream, ct))
                {
                    totalRows++;
                    if (totalRows > MaxAllowedRows)
                        throw new InvalidOperationException($"Файл превышает лимит записей ({MaxAllowedRows})");

                    _validator.Validate(parsedRecord);

                    indicators.Add(parsedRecord.Value);
                    sumExecutionTime += parsedRecord.ExecutionTime;
                    if (parsedRecord.Date < minStartTime) minStartTime = parsedRecord.Date;
                    if (parsedRecord.Date > maxStartTime) maxStartTime = parsedRecord.Date;

                    batch.Add(new ValueRecord
                    {
                        FileName = fileName,
                        StartTime = parsedRecord.Date,
                        ExecutionTime = parsedRecord.ExecutionTime,
                        Indicator = parsedRecord.Value
                    });

                    if (batch.Count >= BatchSize)
                    {
                        _dbContext.Values.AddRange(batch);
                        await _dbContext.SaveChangesAsync(ct);
                        batch.Clear();
                    }
                }

                if (totalRows < MinAllowedRows)
                    throw new InvalidOperationException($"Файл содержит недостаточно записей. Минимум: {MinAllowedRows}");

                if (batch.Count > 0)
                {
                    _dbContext.Values.AddRange(batch);
                    await _dbContext.SaveChangesAsync(ct);
                }

                var integralResult = _calculator.Calculate(
                    fileName, 
                    minStartTime, 
                    maxStartTime, 
                    sumExecutionTime, 
                    indicators);

                _dbContext.Add(integralResult);
                await _dbContext.SaveChangesAsync(ct);

                await transaction.CommitAsync(ct);
            }
            catch
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
        }
    }
}

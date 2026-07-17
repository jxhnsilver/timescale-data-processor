using Microsoft.EntityFrameworkCore;
using TimescaleDataProcessor.Api.Data.Context;
using TimescaleDataProcessor.Api.Dtos;
using TimescaleDataProcessor.Api.Entities;

namespace TimescaleDataProcessor.Api.Services
{
    public class ValuesService : IValuesService
    {
        private readonly ApplicationDbContext _dbContext;

        private const int LimitValues = 10;

        public ValuesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<ValueRecordDto>> GetLatestValuesAsync(CancellationToken ct)
        {
            var query = _dbContext.Values.AsQueryable();

            query = query
                .OrderByDescending(v => v.StartTime)
                .Take(LimitValues)
                .OrderBy(v => v.StartTime)
                .ThenBy(v => v.FileName);

            var values = await query.ToListAsync(ct);

            return values.Select(MapToDto).ToList();
        }

        private static ValueRecordDto MapToDto(ValueRecord value)
        {
            return new ValueRecordDto(
                Id: value.Id,
                FileName: value.FileName,
                StartTime: value.StartTime,
                ExecutionTime: value.ExecutionTime,
                Indicator: value.Indicator
            );
        }
    }
}

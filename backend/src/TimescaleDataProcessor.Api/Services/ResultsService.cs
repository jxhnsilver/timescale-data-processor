using Microsoft.EntityFrameworkCore;
using TimescaleDataProcessor.Api.Data.Context;
using TimescaleDataProcessor.Api.Dtos;
using TimescaleDataProcessor.Api.Entities;

namespace TimescaleDataProcessor.Api.Services
{
    public class ResultsService : IResultsService
    {
        private readonly ApplicationDbContext _dbContext;

        public ResultsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<ResultDto>> GetFilteredResultsAsync(ResultsFilterDto filter)
        {
            var query = _dbContext.Results.AsQueryable();

            if (filter.FileName != null)
                query = query.Where(r => r.FileName == filter.FileName);

            if (filter.StartTimeFrom != null)
                query = query.Where(r => r.MinStartTime >= filter.StartTimeFrom);

            if (filter.StartTimeTo != null)
                query = query.Where(r => r.MinStartTime <= filter.StartTimeTo);

            if (filter.AvgIndicatorFrom != null)
                query = query.Where(r => r.AvgIndicator >= filter.AvgIndicatorFrom);

            if (filter.AvgIndicatorTo != null)
                query = query.Where(r => r.AvgIndicator <= filter.AvgIndicatorTo);

            if (filter.AvgExecutionTimeFrom is not null)
                query = query.Where(r => r.AvgExecutionTime >= filter.AvgExecutionTimeFrom);

            if (filter.AvgExecutionTimeTo is not null)
                query = query.Where(r => r.AvgExecutionTime <= filter.AvgExecutionTimeTo);

            var results = await query.ToListAsync();

            return results.Select(MapToDto).ToList();
        }

        private static ResultDto MapToDto(IntegralResult result)
        {
            return new ResultDto(
                Id: result.Id,
                FileName: result.FileName,
                TimeDeltaInSeconds: result.TimeDeltaInSeconds,
                MinStartTime: result.MinStartTime,
                AvgExecutionTime: result.AvgExecutionTime,
                AvgIndicator: result.AvgIndicator,
                MedianIndicator: result.MedianIndicator,
                MaxIndicator: result.MaxIndicator,
                MinIndicator: result.MinIndicator
            );
        }
    }
}

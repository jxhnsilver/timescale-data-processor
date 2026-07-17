using TimescaleDataProcessor.Api.Dtos;

namespace TimescaleDataProcessor.Api.Services
{
    public interface IResultsService
    {
        Task<IReadOnlyList<ResultDto>> GetFilteredResultsAsync(ResultsFilterDto filter, CancellationToken ct);
    }
}

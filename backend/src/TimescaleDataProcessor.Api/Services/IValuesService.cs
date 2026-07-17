using TimescaleDataProcessor.Api.Dtos;

namespace TimescaleDataProcessor.Api.Services
{
    public interface IValuesService
    {
        Task<IReadOnlyList<ValueRecordDto>> GetLatestValuesAsync(CancellationToken ct);
    }
}

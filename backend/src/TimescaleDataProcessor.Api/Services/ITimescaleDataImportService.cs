namespace TimescaleDataProcessor.Api.Services
{
    public interface ITimescaleDataImportService
    {
        Task ProcessAsync(Stream stream, string fileName, CancellationToken ct);
    }
}

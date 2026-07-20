namespace TimescaleDataProcessor.Api.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}

namespace TimescaleDataProcessor.Api.Dtos
{
    public sealed record RecordDto(
        DateTime Date,
        double ExecutionTime,
        double Value
        );
}

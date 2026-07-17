namespace TimescaleDataProcessor.Api.Dtos
{
    public record ValueRecordDto(
        long Id,
        string FileName,
        DateTime StartTime,
        double ExecutionTime,
        double Indicator
        );
}

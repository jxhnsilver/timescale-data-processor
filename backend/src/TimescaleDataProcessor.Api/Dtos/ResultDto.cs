namespace TimescaleDataProcessor.Api.Dtos
{
    public record ResultDto(
        long Id,
        string FileName,
        double TimeDeltaInSeconds,
        DateTime MinStartTime,
        double AvgExecutionTime,
        double AvgIndicator,
        double MedianIndicator,
        double MaxIndicator,
        double MinIndicator
    );
}

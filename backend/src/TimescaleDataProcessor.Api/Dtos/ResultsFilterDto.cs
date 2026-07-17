namespace TimescaleDataProcessor.Api.Dtos
{
    public record ResultsFilterDto(
        string? FileName,
        DateTime? StartTimeFrom,
        DateTime? StartTimeTo,
        double? AvgIndicatorFrom,
        double? AvgIndicatorTo,
        double? AvgExecutionTimeFrom,
        double? AvgExecutionTimeTo
    );
}

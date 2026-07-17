namespace TimescaleDataProcessor.Api.Dtos
{
    /// <summary>
    /// DTO для фильтрации результатов
    /// </summary>
    /// <param name="FileName">Имя файла</param>
    /// <param name="StartTimeFrom">Левая граница диапазона времени начала операции</param>
    /// <param name="StartTimeTo">Правая граница диапазона времени начала операции</param>
    /// <param name="AvgIndicatorFrom">Левая граница диапазона среднего значения показателя</param>
    /// <param name="AvgIndicatorTo">Правая граница диапазона среднего значения показателя</param>
    /// <param name="AvgExecutionTimeFrom">Левая граница диапазона среднего времени выполнения операции</param>
    /// <param name="AvgExecutionTimeTo">Правая граница диапазона среднего времени выполнения операции</param>
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

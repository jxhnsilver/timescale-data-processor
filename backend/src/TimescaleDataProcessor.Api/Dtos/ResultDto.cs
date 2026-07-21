namespace TimescaleDataProcessor.Api.Dtos
{
    /// <summary>
    /// DTO модели интегрального результата
    /// </summary>
    /// <param name="Id">Уникальный идентификатор интегрального результата</param>
    /// <param name="FileName">Имя файла, по значениям которого был получен интегральный результат</param>
    /// <param name="TimeDeltaInSeconds">Дельта времени StartTime в секундах (максимальное StartTime – минимальное StartTime)</param>
    /// <param name="MinStartTime">Минимальное дата и время, как момент запуска первой операции (StartTime)</param>
    /// <param name="AvgExecutionTime">Среднее время выполнения (ExecutionTime)</param>
    /// <param name="AvgIndicator">Cреднее значение по показателям (Indicator)</param>
    /// <param name="MedianIndicator">Медиана по показателям (Indicator)</param>
    /// <param name="MaxIndicator">Максимальное значение показателя (Indicator)</param>
    /// <param name="MinIndicator">Минимальное значение показателя (Indicator)</param>
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

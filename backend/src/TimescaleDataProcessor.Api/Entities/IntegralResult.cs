namespace TimescaleDataProcessor.Api.Entities
{
    public sealed class IntegralResult
    {
        /// <summary>
        /// Уникальный идентификатор интегрального результата
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Имя файла, по значениям которого был получен интегральный результат
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Дельта времени StartTime в секундах (максимальное StartTime – минимальное StartTime)
        /// </summary>
        public double TimeDeltaInSeconds { get; set; }

        /// <summary>
        /// Минимальное дата и время, как момент запуска первой операции (StartTime)
        /// </summary>
        public DateTime MinStartTime { get; set; }

        /// <summary>
        /// Среднее время выполнения (ExecutionTime)
        /// </summary>
        public double AvgExecutionTime { get; set; }

        /// <summary>
        /// Cреднее значение по показателям (Indicator)
        /// </summary>
        public double AvgIndicator { get; set; }

        /// <summary>
        /// Медиана по показателям (Indicator)
        /// </summary>
        public double MedianIndicator { get; set; }

        /// <summary>
        /// Максимальное значение показателя (Indicator)
        /// </summary>
        public double MaxIndicator { get; set; }

        /// <summary>
        /// Минимальное значение показателя (Indicator)
        /// </summary>
        public double MinIndicator { get; set; }
    }
}

namespace TimescaleDataProcessor.Api.Entities
{
    public sealed class ValueRecord
    {
        /// <summary>
        /// Уникальный идентификатор значения операции
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Имя файла, из которого была получена запись значения операции
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Время начала операции в формате UTC
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время выполнения операции в секундах
        /// </summary>
        public double ExecutionTime { get; set; }
        
        /// <summary>
        /// Числовой показатель
        /// </summary>
        public double Indicator { get; set; }
    }
}

namespace TimescaleDataProcessor.Api.Dtos
{
    /// <summary>
    /// DTO модели значения
    /// </summary>
    /// <param name="Id">Уникальный идентификатор значения операции</param>
    /// <param name="FileName">Имя файла, из которого была получена запись значения операции</param>
    /// <param name="StartTime">Время начала операции в формате UTC</param>
    /// <param name="ExecutionTime">Время выполнения операции в секундах</param>
    /// <param name="Indicator">Числовой показатель</param>
    public record ValueRecordDto(
        long Id,
        string FileName,
        DateTime StartTime,
        double ExecutionTime,
        double Indicator
        );
}

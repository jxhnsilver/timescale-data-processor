using TimescaleDataProcessor.Api.Dtos;

namespace TimescaleDataProcessor.Api.Validators
{
    public class RecordValidator : IRecordValidator
    {
        private readonly DateOnly MinAllowedDate = new(2000, 1, 1);

        public void Validate(RecordDto record)
        {
            var now = DateTime.UtcNow;

            if (record.Date > now)
                throw new ArgumentException($"Дата записи {record.Date} позже текущего времени");

            if (DateOnly.FromDateTime(record.Date) < MinAllowedDate)
                throw new ArgumentException($"Дата записи {record.Date.Date} раньше минимально допустимой даты {MinAllowedDate}");

            if (record.ExecutionTime < 0)
                throw new ArgumentException("Время выполнения не может быть меньше 0");

            if (record.Value < 0)
                throw new ArgumentException("Значение показателя не может быть меньше 0");
        }
    }
}

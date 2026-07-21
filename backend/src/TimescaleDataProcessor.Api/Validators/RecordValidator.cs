using TimescaleDataProcessor.Api.Dtos;
using TimescaleDataProcessor.Api.Exceptions;
using TimescaleDataProcessor.Api.Services;

namespace TimescaleDataProcessor.Api.Validators
{
    public class RecordValidator : IRecordValidator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly DateOnly MinAllowedDate = new(2000, 1, 1);

        public RecordValidator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public void Validate(RecordDto record)
        {
            var now = _dateTimeProvider.UtcNow;

            if (record.Date > now)
                throw new BusinessRuleViolationException($"Дата записи {record.Date} позже текущего времени");

            if (DateOnly.FromDateTime(record.Date) < MinAllowedDate)
            {
                var recordDateOnly = new DateOnly(record.Date.Year, record.Date.Month, record.Date.Day);
                throw new BusinessRuleViolationException($"Дата записи {recordDateOnly} раньше минимально допустимой даты {MinAllowedDate}");
            }

            if (record.ExecutionTime < 0)
                throw new BusinessRuleViolationException("Время выполнения не может быть меньше 0");

            if (record.Value < 0)
                throw new BusinessRuleViolationException("Значение показателя не может быть меньше 0");
        }
    }
}

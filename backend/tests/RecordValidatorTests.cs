using Moq;
using TimescaleDataProcessor.Api.Dtos;
using TimescaleDataProcessor.Api.Exceptions;
using TimescaleDataProcessor.Api.Services;
using TimescaleDataProcessor.Api.Validators;

namespace TimescaleDataProcessor.UnitTests
{
    public class RecordValidatorTests
    {
        private readonly Mock<IDateTimeProvider> _timeProviderMock;
        private readonly RecordValidator _validator;

        private readonly DateOnly MinAllowedDate = new(2000, 1, 1);
        private readonly DateTime MockNow = new DateTime(2026, 7, 20, 12, 0, 0);

        public RecordValidatorTests()
        {
            _timeProviderMock = new Mock<IDateTimeProvider>();
            _timeProviderMock.Setup(t => t.UtcNow).Returns(MockNow);

            _validator = new RecordValidator(_timeProviderMock.Object);
        }

        [Fact]
        public void Validate_DateInFuture_ThrowsBusinessRuleViolationException()
        {
            var record = new RecordDto(
                Date: MockNow.AddSeconds(1),
                ExecutionTime: 20.1,
                Value: 10.1
                );

            var ex = Assert.Throws<BusinessRuleViolationException>(() => _validator.Validate(record));

            Assert.Contains($"Дата записи {record.Date} позже текущего времени", ex.Message);
        }

        [Fact]
        public void Validate_DateEqualsUtcNow_NoException()
        {
            var record = new RecordDto(
                Date: MockNow,
                ExecutionTime: 20.1,
                Value: 10.1
                );
            _validator.Validate(record);
        }

        [Fact]
        public void Validate_DateBeforeMinAllowed_ThrowsBusinessRuleViolationException()
        {
            var record = new RecordDto(
                Date: MinAllowedDate.ToDateTime(TimeOnly.MinValue).AddDays(-1),
                ExecutionTime: 20.1,
                Value: 10.1
                );

            var recordDateOnly = new DateOnly(record.Date.Year, record.Date.Month, record.Date.Day);

            var ex = Assert.Throws<BusinessRuleViolationException>(() => _validator.Validate(record));
            Assert.Contains($"Дата записи {recordDateOnly} раньше минимально допустимой даты {MinAllowedDate}", ex.Message);
        }

        [Fact]
        public void Validate_DateEqualsMinAllowedDate_NoException()
        {
            var record = new RecordDto(
                Date: MinAllowedDate.ToDateTime(TimeOnly.MinValue),
                ExecutionTime: 20.1,
                Value: 10.1
            );

            var exception = Record.Exception(() => _validator.Validate(record));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-100)]
        public void Validate_NegativeExecutionTime_ThrowsBusinessRuleViolationException(double executionTime)
        {
            var record = new RecordDto(
                Date: MockNow.AddDays(-1),
                ExecutionTime: executionTime,
                Value: 10.1
            );

            var ex = Assert.Throws<BusinessRuleViolationException>(() => _validator.Validate(record));
            Assert.Contains("Время выполнения не может быть меньше 0", ex.Message);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-100)]
        public void Validate_NegativeValue_ThrowsBusinessRuleViolationException(double value)
        {
            var record = new RecordDto(
                Date: MockNow.AddDays(-1),
                ExecutionTime: 20.1,
                Value: value
            );

            var ex = Assert.Throws<BusinessRuleViolationException>(() => _validator.Validate(record));
            Assert.Contains("Значение показателя не может быть меньше 0", ex.Message);
        }

        [Fact]
        public void Validate_FullInvalidRecordDto_ThrowsFirstException()
        {
            var record = new RecordDto(
                Date: MockNow.AddDays(1),
                ExecutionTime: -10,
                Value: -0.1
            );

            var ex = Assert.Throws<BusinessRuleViolationException>(() => _validator.Validate(record));
            Assert.Contains($"Дата записи {record.Date} позже текущего времени", ex.Message);
        }

        [Fact]
        public void Validate_ValidRecord_NoException()
        {
            var record = new RecordDto(
                Date: MockNow.AddDays(-1),
                ExecutionTime: 20.1,
                Value: 10.1
            );

            var exception = Record.Exception(() => _validator.Validate(record));
            Assert.Null(exception);
        }
    }
}

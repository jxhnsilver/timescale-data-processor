using TimescaleDataProcessor.Api.Exceptions;
using TimescaleDataProcessor.Api.Services;

namespace TimescaleDataProcessor.UnitTests
{
    public class ResultCalculatorTests
    {
        private readonly ResultCalculator _calculator;

        private readonly string TestFileName = "test_file.csv";
        private readonly DateTime MinStartTime = new DateTime(2026, 7, 20, 10, 0, 0);
        private readonly DateTime MaxStartTime = new DateTime(2026, 7, 20, 11, 0, 0);
        private readonly double SumExecutionTime = 300.0;

        public ResultCalculatorTests()
        {
            _calculator = new ResultCalculator();
        }

        [Fact]
        public void Calculate_EmptyIndicators_ThrowsBusinessRuleViolationException()
        {
            var ex = Assert.Throws<BusinessRuleViolationException>(() =>
                _calculator.Calculate(TestFileName, MinStartTime, MaxStartTime, SumExecutionTime, new List<double>()));

            Assert.Contains("Нет записей для вычисления интегрального результата", ex.Message);
        }

        [Fact]
        public void Calculate_SingleIndicator_ReturnsCorrectResult()
        {
            var indicators = new List<double> { 42.5 };

            var result = _calculator.Calculate(TestFileName, MinStartTime, MaxStartTime, SumExecutionTime, indicators);

            Assert.Equal(TestFileName, result.FileName);
            Assert.Equal(3600.0, result.TimeDeltaInSeconds);
            Assert.Equal(MinStartTime, result.MinStartTime);
            Assert.Equal(300.0, result.AvgExecutionTime);
            Assert.Equal(42.5, result.AvgIndicator);
            Assert.Equal(42.5, result.MedianIndicator);
            Assert.Equal(42.5, result.MinIndicator);
            Assert.Equal(42.5, result.MaxIndicator);
        }

        [Fact]
        public void Calculate_EvenCountIndicators_CorrectMedianAndResult()
        {
            var indicators = new List<double> { 10.0, 20.0, 30.0, 40.0 };

            var result = _calculator.Calculate(TestFileName, MinStartTime, MaxStartTime, SumExecutionTime, indicators);

            Assert.Equal(25.0, result.MedianIndicator); // (20 + 30) / 2 = 25
            Assert.Equal(25.0, result.AvgIndicator);    // (10+20+30+40) / 4 = 25
            Assert.Equal(10.0, result.MinIndicator);
            Assert.Equal(40.0, result.MaxIndicator);
            Assert.Equal(75.0, result.AvgExecutionTime); // 300 / 4 = 75
        }

        [Fact]
        public void Calculate_OddCountIndicators_CorrectMedianAndResult()
        {
            var indicators = new List<double> { 25.0, 15.0, 35.0 };

            var result = _calculator.Calculate(TestFileName, MinStartTime, MaxStartTime, SumExecutionTime, indicators);

            Assert.Equal(25.0, result.MedianIndicator); // indicators после сортировки { 15.0, 25.0, 35.0 }
            Assert.Equal(25.0, result.AvgIndicator);
            Assert.Equal(15.0, result.MinIndicator);
            Assert.Equal(35.0, result.MaxIndicator);
            Assert.Equal(100.0, result.AvgExecutionTime); // 300 / 3 = 100
        }

    }
}

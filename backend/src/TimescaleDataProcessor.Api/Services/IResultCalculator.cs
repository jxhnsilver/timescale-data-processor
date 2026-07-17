using TimescaleDataProcessor.Api.Entities;

namespace TimescaleDataProcessor.Api.Services
{
    public interface IResultCalculator
    {
        IntegralResult Calculate(
            string fileName,
            DateTime minDate,
            DateTime maxDate,
            double sumExecutionTime,
            List<double> indicators);
    }
}

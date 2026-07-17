using TimescaleDataProcessor.Api.Entities;

namespace TimescaleDataProcessor.Api.Services
{
    public class ResultCalculator : IResultCalculator
    {
        public IntegralResult Calculate(
            string fileName,
            DateTime minStartTime,
            DateTime maxStartTime,
            double sumExecutionTime,
            List<double> indicators)
        {
            if (indicators.Count == 0)
                throw new ArgumentException("Нет записей для вычисления интегрального результата");

            var timeDeltaInSeconds = (maxStartTime - minStartTime).TotalSeconds;
            var avgExecutionTime = sumExecutionTime / indicators.Count;
            var avgIndicator = indicators.Average();
            var minIndicator = indicators.Min();
            var maxIndicator = indicators.Max();

            indicators.Sort();
            double medianIndicator;

            if (indicators.Count % 2 == 0)
            {
                var value1 = indicators[indicators.Count / 2];
                var value2 = indicators[indicators.Count / 2 - 1];

                medianIndicator = (value1 + value1) / 2.0;
            }
            else medianIndicator = indicators[indicators.Count / 2];

            return new IntegralResult
            {
                FileName = fileName,
                TimeDeltaInSeconds = timeDeltaInSeconds,
                MinStartTime = minStartTime,
                AvgExecutionTime = avgExecutionTime,
                AvgIndicator = avgIndicator,
                MedianIndicator = medianIndicator,
                MaxIndicator = maxIndicator,
                MinIndicator = minIndicator
            };
        }
    }
}

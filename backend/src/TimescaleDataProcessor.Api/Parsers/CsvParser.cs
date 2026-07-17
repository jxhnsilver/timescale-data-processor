using System.Globalization;
using System.Runtime.CompilerServices;
using TimescaleDataProcessor.Api.Dtos;

namespace TimescaleDataProcessor.Api.Parsers
{
    public class CsvParser : IFileParser
    {
        private const char Delimiter = ';';
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.ffffZ";
        private const string ExpectedHeader = "Date;ExecutionTime;Value";

        public async IAsyncEnumerable<RecordDto> ParseAsync(Stream stream, [EnumeratorCancellation] CancellationToken ct = default)
        {
            using var reader = new StreamReader(stream);

            var headerLine = await reader.ReadLineAsync(ct);
            if (string.IsNullOrWhiteSpace(headerLine))
                throw new InvalidDataException("Файл пуст или заголовок отсутствует");

            if (!headerLine.Equals(ExpectedHeader))
                throw new InvalidDataException($"Неверный формат заголовка. Ожидается: {ExpectedHeader}");

            int lineNumber = 1;
            string? line;

            while ((line = await reader.ReadLineAsync(ct)) != null)
            {
                lineNumber++;

                var parts = line.Split(Delimiter);

                if (!DateTime.TryParseExact(parts[0], DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var date))
                    throw new InvalidDataException($"Строка {lineNumber}: неверный формат даты");

                if (!double.TryParse(parts[1], CultureInfo.InvariantCulture, out var executionTime))
                    throw new InvalidDataException($"Строка {lineNumber}: неверный формат времени выполнения");

                if (!double.TryParse(parts[2], CultureInfo.InvariantCulture, out var value))
                    throw new InvalidDataException($"Строка {lineNumber}: неверный формат значения");

                yield return new RecordDto(date, executionTime, value);
            }
        }
    }
}

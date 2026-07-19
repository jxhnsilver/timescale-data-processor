using TimescaleDataProcessor.Api.Exceptions;

namespace TimescaleDataProcessor.Api.Parsers
{
    public class ParserFactory : IParserFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ParserFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFileParser CreateParser(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
                throw new ArgumentException("Расширение файла не может быть пустым", nameof(fileExtension));

            return fileExtension switch
            {
                ".csv" => _serviceProvider.GetRequiredService<CsvParser>(),
                _ => throw new FileFormatException($"Формат файла '{fileExtension}' не поддерживается")
            };
        }
    }
}

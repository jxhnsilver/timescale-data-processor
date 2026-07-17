using TimescaleDataProcessor.Api.Dtos;

namespace TimescaleDataProcessor.Api.Parsers
{
    public interface IFileParser
    {
        /// <summary>
        /// Асинхронно считывает и парсит данные из потока, возвращая записи по мере их обработки
        /// </summary>
        /// <param name="stream">Поток с данными файла для парсинга</param>
        /// <param name="ct">Токен отмены операции</param>
        /// <returns>Асинхронная последовательность объектов <see cref="RecordDto"/>, лениво возвращаемых при чтении потока</returns>
        IAsyncEnumerable<RecordDto> ParseAsync(Stream stream, CancellationToken ct = default);
    }
}

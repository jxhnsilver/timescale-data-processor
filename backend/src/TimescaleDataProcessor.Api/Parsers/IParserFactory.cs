namespace TimescaleDataProcessor.Api.Parsers
{
    public interface IParserFactory
    {
        /// <summary>
        /// Создает парсер файла для указанного расширения
        /// </summary>
        /// <param name="fileExtension">Расширение файла</param>
        /// <returns>Экземпляр <see cref="IFileParser"/>, соответствующий заданному формату файла</returns>
        IFileParser CreateParser(string fileExtension);
    }
}

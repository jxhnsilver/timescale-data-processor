namespace TimescaleDataProcessor.Api.Exceptions
{
    public class FileFormatException : AppException
    {
        public override string ErrorCode => "file_format_error";

        public FileFormatException(string message) 
            : base(message) { }
    }
}

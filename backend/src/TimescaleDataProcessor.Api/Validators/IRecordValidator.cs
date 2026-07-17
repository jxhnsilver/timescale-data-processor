using TimescaleDataProcessor.Api.Dtos;

namespace TimescaleDataProcessor.Api.Validators
{
    public interface IRecordValidator
    {
        void Validate(RecordDto record);
    }
}

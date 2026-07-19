namespace TimescaleDataProcessor.Api.Exceptions
{
    public class BusinessRuleViolationException : AppException
    {
        public override string ErrorCode => "business_rule_violation";


        public BusinessRuleViolationException(string message) 
            : base(message) { }
    }
}

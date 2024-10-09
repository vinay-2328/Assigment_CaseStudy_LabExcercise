namespace PayXpert.Exception
{
    public class FinancialRecordException : System.Exception
    {
        public FinancialRecordException() : base("Error in managing financial records.")
        {
        }

        public FinancialRecordException(string message) : base(message)
        {
        }

        public FinancialRecordException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}

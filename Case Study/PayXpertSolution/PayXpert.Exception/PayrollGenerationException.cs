namespace PayXpert.Exception
{
    public class PayrollGenerationException : System.Exception
    {
        public PayrollGenerationException() : base("Failed to generate payroll.")
        {
        }

        public PayrollGenerationException(string message) : base(message)
        {
        }

        public PayrollGenerationException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}

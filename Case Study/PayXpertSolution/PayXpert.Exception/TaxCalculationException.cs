namespace PayXpert.Exception
{
    public class TaxCalculationException : System.Exception
    {
        public TaxCalculationException() : base("Failed to calculate tax.")
        {
        }

        public TaxCalculationException(string message) : base(message)
        {
        }

        public TaxCalculationException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}

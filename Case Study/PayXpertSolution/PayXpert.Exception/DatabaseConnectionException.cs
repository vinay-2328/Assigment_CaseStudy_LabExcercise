namespace PayXpert.Exception
{
    public class DatabaseConnectionException : System.Exception
    {
        public DatabaseConnectionException() : base("Error connecting to the database.")
        {
        }

        public DatabaseConnectionException(string message) : base(message)
        {
        }

        public DatabaseConnectionException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}

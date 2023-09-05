namespace BankProject.Exceptions
{
    /// <summary>
    /// Exception class that represents error raised in Customer class
    /// </summary>
    public class CustomerException: ApplicationException
    {
        public CustomerException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Constructor that initializes exception message
        /// </summary>
        /// <param name="message">exception message</param>
        /// <param name="innerException">Inner exception</param>

        public CustomerException(string message, Exception innerException):base(message, innerException) { }
    }
}
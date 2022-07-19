namespace MessengerFrontend.Exceptions
{
    public class RegistrationException : Exception
    {
        public RegistrationException() : base() { }
        public RegistrationException(string message) : base(message) { }
    }
}

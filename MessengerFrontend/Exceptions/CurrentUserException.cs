namespace MessengerFrontend.Exceptions
{
    public class CurrentUserException : Exception
    {
        public CurrentUserException() : base() { }
        public CurrentUserException(string message) : base(message) { }
    }
}

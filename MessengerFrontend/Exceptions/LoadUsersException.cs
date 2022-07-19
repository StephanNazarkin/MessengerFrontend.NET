namespace MessengerFrontend.Exceptions
{
    public class LoadUsersException : Exception
    {
        public LoadUsersException() : base() { }
        public LoadUsersException(string message) : base(message) { }
    }
}

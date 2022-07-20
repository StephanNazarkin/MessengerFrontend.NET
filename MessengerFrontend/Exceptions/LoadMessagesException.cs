namespace MessengerFrontend.Exceptions
{
    public class LoadMessagesException : Exception
    {
        public LoadMessagesException() : base() { }
        public LoadMessagesException(string message) : base(message) { }
    }
}

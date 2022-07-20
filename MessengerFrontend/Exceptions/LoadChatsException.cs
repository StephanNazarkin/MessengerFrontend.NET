namespace MessengerFrontend.Exceptions
{
    public class LoadChatsException : Exception
    {
        public LoadChatsException() : base() { }
        public LoadChatsException(string message) : base(message) { }
    }
}

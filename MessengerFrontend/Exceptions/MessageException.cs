namespace MessengerFrontend.Exceptions
{
    public class MessageException : Exception
    {
        public MessageException() : base() { }
        public MessageException(string message) : base(message) { }
    }
}

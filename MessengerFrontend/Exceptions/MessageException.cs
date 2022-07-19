namespace MessengerFrontend.Exceptions
{
    public class MessageException : Exception
    {
        public ChatroomException() : base() { }
        public ChatroomException(string message) : base(message) { }
    }
}

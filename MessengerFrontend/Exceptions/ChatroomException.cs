namespace MessengerFrontend.Exceptions
{
    public class ChatroomException : Exception
    {
        public ChatroomException() : base() { }
        public ChatroomException(string message) : base(message) { }
    }
}

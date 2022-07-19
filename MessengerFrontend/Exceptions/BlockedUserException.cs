namespace MessengerFrontend.Exceptions
{
    public class BlockedUserException : Exception
    {
        public BlockedUserException() : base() { }
        public BlockedUserException(string message) : base(message) { }
    }
}

namespace MessengerFrontend.Exceptions
{
    public class FriendUserException : Exception
    {
        public FriendUserException() : base() { }
        public FriendUserException(string message) : base(message) { }
    }
}

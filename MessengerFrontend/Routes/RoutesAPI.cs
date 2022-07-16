namespace MessengerFrontend.Routes
{
    public static class RoutesAPI
    {
        #region Account
        public const string Register = "Account/Register";

        public const string Login = "Account/Login";

        public const string GetAllFriends = "Account/GetAllFriends";

        public const string GetAllBlockedUsers = "Account/GetAllBlockedUsers";

        public const string GetCurrentUser = "Account/GetCurrentUser";

        public const string GetAllUsers = "Account/GetAllUsers";

        public const string AddFriend = "Account/AddFriend";

        public const string DeleteFriend = "Account/DeleteFriend?friendId=";

        public const string BlockUser = "Account/BlockUser";

        public const string UnblockUser = "Account/UnblockUser?blockedUserId=";

        public const string UpdateUser = "Account/UpdateUser";

        public const string GetUserByUserName = "/Account/GetUserByUserName?userName=";
        #endregion

        #region Chatroom
        public const string GetAllChatrooms = "Chatroom/GetAllChatrooms";

        public const string CreateChatroom = "Chatroom/CreateChatroom";

        public const string EditChatroom = "Chatroom/EditChatroom";

        public const string SoftDeleteChatroom = "Chatroom/SoftDeleteChatroom";

        public const string AddToChatroom = "Chatroom/AddToChatroom";

        public const string ChatSetAdmin = "Chatroom/SetAdmin";

        public const string ChatUnsetAdmin = "Chatroom/UnsetAdmin";

        public const string ChatBanUser = "Chatroom/BanUser";

        public const string ChatUnbanUser = "Chatroom/UnbanUser";

        public const string ChatKickUser = "Chatroom/KickUser";

        public const string LeaveFromChatroom = "Chatroom/LeaveFromChatroom";

        public const string GetCurrentUserAccount = "Chatroom/GetCurrentUserAccount?chatId=";

        public const string GetAllUsersFromChat = "Chatroom/GetAllUsers?chatId=";

        public const string GetChatroom = "Chatroom/GetChatroom?chatId=";
        #endregion

        #region Message
        public const string SendMessage = "Message/SendMessage";

        public const string EditMessage = "Message/EditMessage";

        public const string SoftDeleteMessage = "Message/SoftDeleteMessage";

        public const string GetMessage = "Message/GetMessage?messageId=";

        public const string GetMessagesFromChat = "Message/GetMessagesFromChat?chatId=";
        #endregion
    }
}

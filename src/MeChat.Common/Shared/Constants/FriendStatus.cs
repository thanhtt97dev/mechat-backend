namespace MeChat.Common.Shared.Constants;

public partial class AppConstants
{
    public class FriendStatus
    {
        public const int UnFriend = 0;
        public const int WatitingAccept = 1;
        public const int Accepted = 2;
        public const int Block = 3;
    }

    public class FriendRealtionship : FriendStatus
    {
        public const int FriendRequest = 4;
    }
}
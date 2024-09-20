namespace MeChat.Common.Shared.Constants;

public partial class AppConstants
{
    public class FriendStatus
    {
        public const int UnFriend = 1;
        public const int WatitingAccept = 2;
        public const int Accepted = 3;
        public const int Block = 4;

    }

    public class FriendRealtionship : FriendStatus
    {
        public const int FriendRequest = 5;
        public const int BlockRequester = 6;
    }

    public class FriendStatusRequest : FriendStatus
    {
        public const int RequestUnBlock = 7;

        public static int[] STATUS = new int[] { UnFriend, WatitingAccept, Accepted, Block , RequestUnBlock };
    }
}
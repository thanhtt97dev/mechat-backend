using MeChat.Common.Shared.Constants;

namespace MeChat.Common.UseCases.V1.User;
public class Response
{
    public record User(Guid Id, string RoleId, string Email, string Avatar, string Fullname, string Username, string CoverPhoto);

    #region UserPublicInfo
    public record UserPublicInfo
    {
        public Guid? Id { get; init; }
        public string? RoleId { get; init; }
        public string? Email { get; init; }
        public string? Avatar { get; init; }
        public string? CoverPhoto { get; init; }
        public string? Fullname { get; init; }
        public string? Username { get; init; }
        public int? RelationshipStatus { get; init; } = AppConstants.FriendStatus.UnFriend;
        public int? TotalFriends { get; init; } = 0;
        public List<FriendInfo>? Friends { get; init; } = new();
        public UserPublicInfo(){ }

        #region FriendInfo
        public class FriendInfo
        {
            public Guid Id { get; init; }
            public string? Username { get; init; }
            public string? Fullname { get; init; }
            public string? Avatar { get; init;}
        }
        #endregion

    }
    #endregion

}

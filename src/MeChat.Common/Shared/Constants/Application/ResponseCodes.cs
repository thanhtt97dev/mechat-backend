namespace MeChat.Common.Shared.Constants;

public partial class AppConstants
{
    public class ResponseCodes
    {
        #region Common
        public const string Success = "00";
        public const string Failure = "01";
        public const string NotFound = "02";
        public const string ValidationError = "03";
        public const string UnAuthorized = "04";
        public const string UnAuthentication = "05";
        #endregion

        public class User
        {
            public const string Banned = "1001";

            public const string WrongPassword = "2001";
            public const string UsernameExisted = "2002";

            public const string FriendBlock = "3001";
        }
    }
}


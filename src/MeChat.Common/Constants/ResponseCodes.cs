namespace MeChat.Common.Constants;

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

        #region User
        public const string UserBanned = "1001";
        #endregion
    }
}


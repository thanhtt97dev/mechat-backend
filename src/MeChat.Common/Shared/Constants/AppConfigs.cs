namespace MeChat.Common.Shared.Constants;

public partial class AppConstants
{
    public class AppConfigs
    {
        public class RequestHeader
        {
            public const string USER_ID = "x_mechat_u_id";
            public const string CONFRIM_SIGN_UP_TOKEN = "x_mechat_sign_up";
        }

        public class Jwt
        {
            public const string ID = "id";
            public const string ROLE = "role";
            public const string EMAIL = "x-email";
            public const string JTI = "jti";
            public const string EXPIRED = "expired";
        }
    }
}

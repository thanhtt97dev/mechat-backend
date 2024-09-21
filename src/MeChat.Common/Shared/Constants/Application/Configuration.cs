namespace MeChat.Common.Shared.Constants;

public partial class AppConstants
{
    public class Configuration
    {
        public class RequestHeader
        {
            public const string id = "x_mechat_u_id";
            public const string confirmSignUpToken = "x_mechat_sign_up";
        }

        public class Jwt
        {
            public const string id = "id";
            public const string role = "role";
            public const string email = "x-email";
            public const string jti = "jti";
            public const string expired = "expired";
        }
    }
}

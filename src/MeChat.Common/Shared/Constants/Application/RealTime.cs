namespace MeChat.Common.Shared.Constants;
public partial class AppConstants
{
    public class RealTime
    {
        public class Method
        {
            public const string Notification = "notification";
        }

        public class Endpoint
        {
            public const string Root = "realtime";
            public const string Connection = $"/{Root}/connection";
        }
    }
}

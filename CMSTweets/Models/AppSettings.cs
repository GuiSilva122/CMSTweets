using System;

namespace CMSTweets
{
    public class AppSettings
    {
        public String Consumer_Key { get; set; }
        public String Consumer_Secret { get; set; }
        public String AccessToken { get; set; }
        public String AccessTokenSecret { get; set; }
        public Int32 NumberOfResults { get; set; }
        public Int32 NumberOfSecondsToCheckNewTweets { get; set; }
    }
}

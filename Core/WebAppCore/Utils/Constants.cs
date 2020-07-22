namespace WebAppCoreNew.Utils
{
    public class Constants
    {
        public const string REDIS_KEY_USER_GETPAGING = "redis_key";
        public const string REDIS_KEY_MENU_GETMENUS = "REDIS_KEY_MENU_GETMENUS";
        public const string REDIS_KEY_HOME_PAGE_GETGOLDENS = "REDIS_KEY_HOME_PAGE_GETGOLDENS";
        public const string REDIS_KEY_GET_RATE = "REDIS_KEY_GET_RATE";
        public const string REDIS_KEY_GET_INTEREST_RATE = "REDIS_KEY_GET_INTEREST_RATE";
        public const string REDIS_KEY_GET_COMPARE_INTEREST_RATE = "REDIS_KEY_GET_COMPARE_INTEREST_RATE";
        public const string REDIS_KEY_GETCATEBYID = "REDIS_KEY_GETCATEBYID";
        public const string REDIS_KEY_GETRATEBYCODE = "REDIS_KEY_GETRATEBYCODE";
        public const string REDIS_KEY_GETOILES = "REDIS_KEY_GETOILES";
        public const string REDIS_KEY_GETOILES_TOP6 = "REDIS_KEY_GETOILES_TOP6";
        public const string REDIS_KEY_GETOILES_BYYEARS = "REDIS_KEY_GETOILES_BYYEARS";
        public const string REDIS_KEY_APPSETTING = "REDIS_KEY_APPSETTING";
        public const string REDIS_KEY_APPSETTING_FOOTER = "REDIS_KEY_APPSETTING_FOOTER";
        public const string REDIS_KEY_CRYPTO_GETCRYPTOCURRENCY = "REDIS_KEY_CRYPTO_GETCRYPTOCURRENCY";
        public const string REDIS_KEY_BLOG_LIST = "REDIS_KEY_BLOG_LIST";
        public const string REDIS_KEY_GOLDEN_CHART = "REDIS_KEY_GOLDEN_CHART";
        public const string REDIS_KEY_GET_ADVERTISING = "REDIS_KEY_GET_ADVERTISING";
        public const string REDIS_KEY_GETGOLDBANK = "REDIS_KEY_GETGOLDBANK";
        public const string REDIS_KEY_GETGOLDCATEBYID = "REDIS_KEY_GETGOLDCATEBYID";
        public const string REDIS_KEY_GETOTHERCATEBYID = "REDIS_KEY_GETOTHERCATEBYID";
        public const string REDIS_KEY_GETNEWSRIGHT = "REDIS_KEY_GETNEWSRIGHT";
        public const string REDIS_KEY_GETGOLDBANKCAT = "REDIS_KEY_GETGOLDBANKCAT";
        public const string REDIS_KEY_SEOHOMEPAGE = "REDIS_KEY_SEOHOMEPAGE";

        public const string WEBSITE_NAME = "Tỷ Giá";
        public const string FACEBOOK_APP_ID = "788105437921828";
        public static int PAGE_SIZE = 20;
    }

    public enum CategoryType
    {
        GoldenWorld = 1,
        GoldenSJC = 2
    }
}

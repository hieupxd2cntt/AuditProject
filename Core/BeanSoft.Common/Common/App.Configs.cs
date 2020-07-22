using System.Configuration;

namespace Core.Common
{
    public static partial class App
    {
        public static class Configs
        {
            public static string ConnectionString { get; set; }
            public static string ServiceUri { get; set; }
            public static string ServiceUriHttp { get; set; }  
            public static string UpdatedToDateVersion { get; set; }
            public static string UseProxy { get; set; }
            public static string ProxyUrl { get; set; }
            public static string ProxyUser { get; set; }
            public static string ProxyPass { get; set; }
            public static string ExcelHomeDirectory { get; set; }
            public static string LocalDB { get; set; }
            public static string IsPOS { get; set; }

            static Configs()
            {
                ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                ServiceUri = ConfigurationManager.AppSettings["ServiceUri"];
                ServiceUriHttp = ConfigurationManager.AppSettings["ServiceUriHttp"];
                UseProxy = ConfigurationManager.AppSettings["UseProxy"];
                ProxyUrl = ConfigurationManager.AppSettings["proxyUrl"];
                ProxyUser = ConfigurationManager.AppSettings["proxyUser"];
                ProxyPass = ConfigurationManager.AppSettings["proxyPass"];
                ExcelHomeDirectory = ConfigurationManager.AppSettings["ExcelHomeDirectory"];
                LocalDB = string.Format("data source={0}", ConfigurationManager.AppSettings["LocalDB"]);
                IsPOS = ConfigurationManager.AppSettings["IsPOS"];
                
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using WebCore.Entities;

namespace Core.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string con = "User ID=core;Password=core;Host=20.36.36.172;Port=5432;Database=core;Pooling=true;Command Timeout	=3000;";

            List<string> value = new List<string>();
            List<LanguageInfo> languageInfos = new List<LanguageInfo>();
            languageInfos = PostgresqlHelper.ExecuteStoreProcedure<LanguageInfo>(con, null, "core.getallmodule", null);
        }
    }
}

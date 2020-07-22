using System;
using System.Text;
using Microsoft.AspNetCore.Html;
using WebAppCoreNew.Utils;

namespace Microsoft.AspNetCore.Mvc
{
    public static class HtmlHelpers
    {        

        /// <summary>
        /// Author : Jindo.vu
        /// CreatedDate : 16-09-2015
        /// Description : Function use to cut word from text
        /// </summary>
        /// <param name="input">input need cut</param>
        /// <param name="startIndex">start index  word</param>
        /// <param name="endIndex">end Index  word</param>
        /// <returns></returns>
        public static string CutWordFromString(string input, int startIndex, int endIndex)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(input))
            {
                var arrayList = input.Split(' ');
                if (endIndex < arrayList.Length - 1)
                {
                    for (var i = startIndex; i <= endIndex; i++)
                    {
                        result += " " + arrayList[i];
                    }
                    return result + "...";
                }
                result = input;
            }

            return result;
        }
    }
}
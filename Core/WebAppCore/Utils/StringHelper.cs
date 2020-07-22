using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebAppCoreNew.Utils
{
    public static class StringHelper
    {
        private static readonly string[] VietnameseSigns = {
                                        "aAeEoOuUiIdDyY",

                                        "áàạảãâấầậẩẫăắằặẳẵ",

                                        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

                                        "éèẹẻẽêếềệểễ",

                                        "ÉÈẸẺẼÊẾỀỆỂỄ",

                                        "óòọỏõôốồộổỗơớờợởỡ",

                                        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

                                        "úùụủũưứừựửữ",

                                        "ÚÙỤỦŨƯỨỪỰỬỮ",

                                        "íìịỉĩ",

                                        "ÍÌỊỈĨ",

                                        "đ",

                                        "Đ",

                                        "ýỳỵỷỹ",

                                        "ÝỲỴỶỸ"
                                    };

        public static int CountWords(string strContent)
        {
            if (String.IsNullOrEmpty(strContent))
                return 0;
            strContent = strContent.Replace("  ", " ");
            var arr = strContent.Split(' ');
            return arr.Count();
        }

        public static string FormatMoney(decimal money)
        {
            return string.Format("{0:C}", money);
        }

        public static string FormatMoneyNoIcon(decimal money)
        {
            var result = string.Format("{0:N}", money);
            if (result.Contains(".00"))
            {
                result = result.Replace(".00", "");
            }
            return result;
        }

        public static string FormatMoneyNoDecimal(decimal money)
        {
            var result = string.Format("{0:N}", money);
            if (result.Contains("."))
            {
                var listResult = result.Split(".");
                if (listResult.Length > 0)
                    return listResult[0];
            }
            return result;
        }

        public static int CountImages(string strContent)
        {
            if (String.IsNullOrEmpty(strContent))
                return 0;
            strContent = strContent.Replace("_", "");
            strContent = strContent.Replace("<img", "_");
            var arr = strContent.Split('_');
            return arr.Count() - 1;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public static string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

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
                var text = StripHtml(input);
                var arrayList = text.Split(' ');
                if (endIndex <= arrayList.Length - 1)
                {
                    for (var i = startIndex; i <= endIndex; i++)
                    {
                        result += " " + arrayList[i];
                    }
                }
                else
                {
                    result = input;
                }
            }

            return StripHtml(result) + "...";
        }

        /// <summary>
        /// Author      : Jindo.vu
        /// CreatedDate : 22-09-2015
        /// Description : function use to convert title utf-8 of page into character normal 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string ConvertTitleForPage(string title)
        {
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            var strReturn = Regex.Replace(title.Trim(), "[^\\w\\s]", string.Empty).Replace(" ", "-").ToLower();
            var strFormD = strReturn.Normalize(NormalizationForm.FormD);

            return regex.Replace(strFormD, string.Empty).Replace("đ", "d");

        }

        public static int SplitPage(string strText)
        {
            var page = 0;
            if (!string.IsNullOrEmpty(strText))
            {
                page = Convert.ToInt32(strText.Split('-').Last());
            }

            return page;
        }

        public static string SplitTag(string strText)
        {
            var tagName = string.Empty;
            if (!string.IsNullOrEmpty(strText))
            {
                var arrayListTag = strText.Split('/')[3].Split('-');

                for (int i = 1; i < arrayListTag.Count(); i++)
                {
                    tagName += arrayListTag[i] + " ";
                }
            }

            return tagName;
        }

        /// <summary>
        /// Removes multiple whitespace characters from a string.
        /// </summary>
        /// <param name="text">
        /// </param>
        /// <returns>
        /// The remove multiple whitespace.
        /// </returns>
        public static string RemoveMultipleWhitespace(string text)
        {
            string result = String.Empty;
            if (String.IsNullOrEmpty(text))
            {
                return result;
            }

            var r = new Regex(@"\s+");
            return r.Replace(text, @" ");
        }

        public static bool IsContainsHtmlTag(string text)
        {
            Regex regex = new Regex(@"<(.|\n)*?>", RegexOptions.IgnoreCase);

            return regex.IsMatch(text);
        }

        /// <summary>
        /// Removes the script tag.
        /// </summary>
        /// <param name="strData">Doan text can remove</param>
        /// <returns></returns>
        public static string RemoveScriptTag(string strData)
        {
            strData = Regex.Replace(strData, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return strData;
        }

        /// <summary>
        /// Loại bỏ dấu
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSign4VietnameseString(string str)
        {
            //remove 
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }

            return str;

        }

        /// <summary>
        /// friendly url
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToFriendlyUrl(string str)
        {
            if (string.IsNullOrEmpty(str)) 
                return string.Empty;

            var strTitle = str.ToLower();
            var rgx = new Regex("[~?!@#$%^&*()_+/|\\/><\\]\\[.,'\";:-]");

            var result = ReplaceCharacterSpecial(strTitle);
            var strRemoveVn = RemoveSign4VietnameseString(result);
            result = rgx.Replace(strRemoveVn, " ").Trim().Replace(' ', '-');

            return result.Contains("--") ? result.Replace("--", "-") : result;
        }

        public static string ReplaceCharacterSpecial(string input)
        {
            const string strRegex = "”,“,–,&"; // array character need remove
            if (string.IsNullOrEmpty(input))
                return input;

            var arrayRegex = strRegex.Split(',');
            if (arrayRegex.Length == 0)
                return input;

            foreach (var item in arrayRegex.Where(item => input != null && input.Contains(item)))
            {
                input = input.Replace(item, "");
            }
            return input;
        }

        /// <summary>
        /// Su dung ham constain co su dung comparison
        /// </summary>
        /// <param name="original"></param>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static bool Contains(this string original, string value, StringComparison comparisonType)
        {
            return original.IndexOf(value, comparisonType) >= 0;
        }

        /// <summary>
        /// Format một String
        /// </summary>
        /// <param name="str"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public static string Frmat(this string str, params object[] @params)
        {
            return string.Format(str, @params);
        }

        /// <summary>
        /// Kiểm tra xem string có Null hay không
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNull(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Kiểm tra string nó null hay ko
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNull(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// swith giá trị khi mà str là Empty
        /// </summary>
        /// <param name="str"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string WhenEmpty(this string str, Func<string> action)
        {
            return str.IsNull() ? action() : str;
        }

        /// <summary>
        /// format date theo định dạng dd/MM/yyyy - HH:mm
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy - HH:mm");
        }

        public static string FormatDateVn(DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string MappingImageUrl(string url, int width, int height)
        {
            //var configuration = ConfigurationManager.AppSettings["MediaResize"];
            //return !string.IsNullOrEmpty(configuration) ? string.Concat(configuration, url, "?w=" + width, "&h=" + height) : url;
            return "";
        }

        public static object MappingImageUrl(string url)
        {
            //var configuration = ConfigurationManager.AppSettings["MediaResize"];
            //return !string.IsNullOrEmpty(configuration) ? string.Concat(configuration, url) : url;
            return "";
        }

        public static string FormatGoldenMoney(decimal cateBuy)
        {
            var result = string.Format("{0:N}", cateBuy);
            if (result.Contains(".00"))
            {
               result = result.Replace(".00", "");
            }
            return result;
        }
    }
}

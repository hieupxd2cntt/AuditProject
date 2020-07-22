namespace WebAppCoreNew.Utils
{
    public static class UrlFriendlyHelper
    {
        public static string RootGolden = "gia-vang";
        public static string RootGoldenSjc = "gia-vang/sjc";
        public static string RootGoldenChart = "gia-vang/bieu-do";
        public static string RootRate = "ty-gia";
        public static string RootChartRate = "ty-gia/bieu-do";
        public static string RootCurrency = "ngoai-te";
        public static string RootCryptoCurrency = "tien-ao";
        public static string RootCryptoCurrencyChart = "tien-ao/trading-view";
        public static string RootOil = "gia-xang-dau";
        public static string RootOilPetro = "gia-xang-dau/petrolimex";
        public static string RootBlog = "blog";
        public static string RootBlogDetail = "chi-tiet";
        public static string RootInterestRate = "lai-suat";
        public static string RootContact = "lien-he";

        public static string RootCompany = "cong-ty";
        public static string Recruitment = "tuyen-dung";
        public static string RootImage = "tin-tuc-hinh-anh";
        public static string RootSearch = "search";
        public static string RootRateChart = "bieu-do-ty-gia";


        public static string RootGoldBank = "gia-vang-bank";

        public static string GetRootChartRate()
        {
            return string.Format("/{0}", RootChartRate);
        }

        public static string GetRootGoldBank()
        {
            return string.Format("/{0}", RootGoldBank);
        }

        public static string GetDetailGoldBank(string code)
        {
            return string.Format("/{0}/{1}", RootGoldBank, code);
        }

        public static string GetRateDetailUrl(string title, long id)
        {
            return GetUrl(RootRate, title, id);
        }

        public static string GetGoldenTheWorld()
        {
            return GetUrl(RootGolden, "the-gioi");
        }

        public static string GetVideoDetailUrl(string title, int id)
        {
            return GetUrl(RootCryptoCurrency, title, id);
        }

        public static string GetGoldenDetailUrl(string title)
        {
            return GetUrl(RootGolden, title);
        }

        public static string GetRootGoldenUrl()
        {
            return string.Format("/{0}", RootGolden);
        }

        public static string GetGoldenChartUrl(string title, long id)
        {
            return GetUrl(RootGoldenChart, title, id);
        }

        public static string GetGoldenByDateUrl(string code, string date)
        {
            return GetUrl(RootGolden, code, date);
        }

        public static string GetDetailCyptoConcurrency(string code)
        {
            return GetUrl(RootCryptoCurrency, code);
        }

        public static string GetChartCyptoConcurrency(string code)
        {
            return GetUrl(RootCryptoCurrencyChart, code);
        }

        public static string GetRateDetailUrl(string code)
        {
            return GetUrl(RootRate, code);
        }

        private static string GetUrl(string type, string title, long id)
        {
            return string.Format("/{0}/{1}-{2}", type, StringHelper.ToFriendlyUrl(title).ToLower(), id.ToString().ToLower());
        }

        private static string GetUrlPage(string type, string title, long id, int page)
        {
            return string.Format("/{0}/{1}-{2}?page={3}", type, StringHelper.ToFriendlyUrl(title).ToLower(), id.ToString().ToLower(), page);
        }

        public static string GetListProductCate(int cateId, string title, string attr)
        {
            return GetUrl(RootCurrency, title, cateId, attr);
        }

        private static string GetUrl(string rootCateProduct, string title, long cateId, string tag)
        {
            if (!string.IsNullOrEmpty(tag))
            {
                if (!string.IsNullOrEmpty(tag))
                {
                    tag = ConvertToCharacterSpecial(tag);
                }
                return string.Format("/{0}/{1}-{2}/{3}", rootCateProduct, StringHelper.ToFriendlyUrl(title), cateId, tag);
            }
            else
            {
                return string.Format("/{0}/{1}-{2}", rootCateProduct, StringHelper.ToFriendlyUrl(title), cateId);
            }
        }

        private static string ConvertToCharacterSpecial(string input)
        {
            string result;
            if (input.Contains("/"))
            {
                result = input.Replace("/", "_");
            }
            else if (input.Contains(@"\"))
            {
                result = input.Replace(@"\", "-");
            }
            else
            {
                result = input;
            }
            return result;
        }

        public static string GetDetailCurrency(string code)
        {
            return GetUrl(RootCurrency, code);
        }

        public static string GetListNews()
        {
            return string.Format("/{0}", RootGolden);
        }

        public static string GetCategoryBlogUrl(string title, long id)
        {
            return GetUrl(RootBlog, title, id);
        }

        public static string GetCategoryBlogSearch(string tag, int? page)
        {
            return GetUrlPageSearch(RootBlog, tag, page);
        }

        private static string GetUrlPageSearch(string type, string tag, int? page)
        {
            return string.Format("/{0}/{1}?page={2}", type, tag, page);
        }

        public static string GetCategoryBlogUrl(string title, long id, int page)
        {
            return GetUrlPage(RootBlog, title, id, page);
        }

        public static string GetDetailBlogUrl(string title, long id)
        {
            return GetUrl(RootBlogDetail, title, id);
        }

        public static string GetRootInterestRate()
        {
            return string.Format("/{0}", RootInterestRate);
        }

        public static string GetDetailInterestRate(string code)
        {
            return string.Format("/{0}/{1}", RootInterestRate, code);
        }

        public static string GetRootInterestRateByCode(string code)
        {
            return string.Format("/{0}/{1}", RootInterestRate, code);
        }

        public static string GetContact()
        {
            return string.Format("/{0}", RootContact);
        }

        public static string GetListCompany()
        {
            return string.Format("/{0}", RootCompany);
        }

        public static string GetCompanyDetailUrl(string title, long id)
        {
            return GetUrl(RootCompany, title, id);
        }

        public static string GetNewImage()
        {
            return string.Format("/{0}", RootImage);
        }

        #region private method

        private static string GetUrl(string titlePage, long id)
        {
            return string.Format("/{0}-{1}", StringHelper.ToFriendlyUrl(titlePage), id);
        }

        private static string GetUrl(string rootPage, string titlePage)
        {
            return string.Format("/{0}/{1}", rootPage, StringHelper.ToFriendlyUrl(titlePage));
        }

        private static string GetUrl(string rootPage, string code, string search)
        {
            return string.Format("/{0}/{1}/{2}", rootPage, code.ToLower(), search);
        }

        #endregion

        public static string GetRecruitment()
        {
            return string.Format("/{0}", Recruitment);
        }

        public static string GetRecruitDetailUrl(string title, long id)
        {
            return GetUrl(Recruitment, title, id);
        }

        public static string GetListVideo(string title, int id)
        {
            return GetUrl(RootCryptoCurrency, title, id);
        }

        public static string GetListCatalogue(string strTitle, int id)
        {
            return GetUrl(RootOil, strTitle, id);
        }

        public static string GetDetailOilUrl(int year)
        {
            return string.Format("/{0}/{1}", RootOilPetro, year);
        }

        public static string GetDetailOilUrl(int year, int month)
        {
            return string.Format("/{0}/{1}-{2}", RootOilPetro, year, month);
        }

        public static string GetRootOilUrl()
        {
            return string.Format("/{0}", RootOilPetro);
        }

        public static string GetRootCurrencyUrl()
        {
            return string.Format("/{0}", RootCurrency);
        }

        public static string GetIndexBlogUrl()
        {
            return string.Format("/{0}", RootBlog);
        }
    }
}
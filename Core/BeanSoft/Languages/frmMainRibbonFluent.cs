using AppClient.Interface;
using Core.Utils;

namespace AppClient
{
    public partial class frmMainRibbonFluent
    {
        internal class frmMainRibbonFluentLanguage : IMainLanguage
        {
            public string ApplicationTitle { get; set; }
            public string ExitTitle { get; set; }
            public string ExitConfirm { get; set; }
            public string LogoutTitle { get; set; }
            public string LogoutConfirm { get; set; }
        }

        internal frmMainRibbonFluentLanguage Language { get; set; }

        public void InitializeLanguage()
        {
            Language = new frmMainRibbonFluentLanguage
            {
                ApplicationTitle = LangUtils.TranslateBasic("APP.Title", "APP.Title"),
                ExitTitle = LangUtils.TranslateBasic("APP$EXIT.Title", "APP$EXIT.Title"),
                ExitConfirm = LangUtils.TranslateBasic("APP$EXIT.Confirm", "APP$EXIT.Confirm"),
                LogoutTitle = LangUtils.TranslateBasic("APP$EXIT.Title", "APP$EXIT.Title"),
                LogoutConfirm = LangUtils.TranslateBasic("APP$EXIT.Confirm", "APP$EXIT.Confirm")
            };
        }
    }
}

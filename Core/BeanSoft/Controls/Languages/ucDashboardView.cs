using Core.Entities;

namespace AppClient.Controls
{
    public partial class ucDashboardView
    {

        class DashboardViewLanguage : ModuleLanguage
        {
            public string Layout { get; set; }
            public DashboardViewLanguage(ModuleInfo moduleInfo) : base(moduleInfo)
            {
            }
        }

        private new DashboardViewLanguage Language
        {
            get
            {
                return (DashboardViewLanguage)base.Language;
            }
        }

        public override void InitializeLanguage()
        {
            base.Language = new DashboardViewLanguage(ModuleInfo);
            Language.Layout = Language.GetLayout(null);
            base.InitializeLanguage();
        }
    }
}

using Core.Entities;
using Core.Utils;

namespace AppClient.Controls
{
    public partial class ucLogin
    {
        class LoginLanguage : ModuleLanguage
        {
            public LoginLanguage(ModuleInfo moduleInfo)
                : base(moduleInfo)
            {
            }
        }

        public override void InitializeLanguage()
        {
            Language = new LoginLanguage(ModuleInfo);
            base.InitializeLanguage();
        }
    }
}

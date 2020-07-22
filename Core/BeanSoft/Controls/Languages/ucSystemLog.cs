using Core.Entities;

namespace AppClient.Controls
{
    public partial class ucSystemLog
    {
        class SystemLogLanguage : ModuleLanguage
        {
            public SystemLogLanguage(ModuleInfo moduleInfo)
                : base(moduleInfo)
            {
            }
        }

        private new SystemLogLanguage Language
        {
            get
            {
                return (SystemLogLanguage)base.Language;
            }
        }
        
        public override void InitializeLanguage()
        {
            base.Language = new SystemLogLanguage(ModuleInfo);
            base.InitializeLanguage();
        }
    }
}

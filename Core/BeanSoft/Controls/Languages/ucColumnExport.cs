using Core.Entities;
using AppClient.Utils;

namespace AppClient.Controls
{
    public partial class ucColumnExport
    {
        class ColumnExportLanguage : ModuleLanguage
        {
            public ColumnExportLanguage(ModuleInfo moduleInfo)
                : base(moduleInfo)
            {
            }
        }

        private new ColumnExportLanguage Language
        {
            get
            {
                return (ColumnExportLanguage)base.Language;
            }
        }

        public override void InitializeLanguage()
        {
            base.Language = new ColumnExportLanguage(ModuleInfo)
            {
            };
            base.InitializeLanguage();
        }
    }
}

using Core.Entities;
using Core.Utils;

namespace AppClient.Controls
{
    public partial class ucWorkFlowMaster
    {
        class WorkFlowMasterLanguage : ModuleLanguage
        {
            public string AddTitle { get; set; }
            public string EditTitle { get; set; }
            public string ViewTitle { get; set; }
            public string AddLayout { get; set; }
            public string EditLayout { get; set; }
            public string ViewLayout { get; set; }
            public string SuccessStatus { get; set; }

            public WorkFlowMasterLanguage(ModuleInfo moduleInfo)
                : base(moduleInfo)
            {
            }
        }

        private new WorkFlowMasterLanguage Language
        {
            get
            {
                return (WorkFlowMasterLanguage)base.Language;
            }
        }

        public override void InitializeLanguage()
        {
            base.Language = new WorkFlowMasterLanguage(ModuleInfo)
            {
                AddTitle = LangUtils.TranslateModuleItem(LangType.MODULE_TEXT, ModuleInfo, "Add"),
                EditTitle = LangUtils.TranslateModuleItem(LangType.MODULE_TEXT, ModuleInfo, "Edit"),
                ViewTitle = LangUtils.TranslateModuleItem(LangType.MODULE_TEXT, ModuleInfo, "View"),
            };

            Language.SuccessStatus = Language.GetSpecialStatus("Success");
            Language.FormatButton(btnCommit, "BTN_COMMIT");
            Language.FormatButton(btnClose, "BTN_CLOSE");
            Language.AddLayout = Language.GetLayout("Add");
            Language.EditLayout = Language.GetLayout("Edit");
            Language.ViewLayout = Language.GetLayout("View");            
            try {
                MaintainModuleInfo moduleinfo = (MaintainModuleInfo)ModuleInfo;                
            }
            catch 
            { 
            
            }
            
            base.InitializeLanguage();
        }
    }
}

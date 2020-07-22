using Core.Entities;
using Core.Utils;

namespace AppClient.Controls
{
    public partial class ucUploadFile 
    {

        class UploadFileLanguage : ModuleLanguage
        {
            public string Layout { get; set; }
            public UploadFileLanguage(ModuleInfo moduleInfo)
                : base(moduleInfo)
            {
            }
        }
        private new UploadFileLanguage Language
        {
            get
            {
                return (UploadFileLanguage)base.Language;
            }
        }


        public override void InitializeLanguage()
        {
            base.Language = new UploadFileLanguage(ModuleInfo);

            Language.Layout = Language.GetLayout(null);
            Language.FormatButton(btnUpload);
            base.InitializeLanguage();
        }
    }
}

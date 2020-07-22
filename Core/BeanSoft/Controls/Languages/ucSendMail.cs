﻿using Core.Entities;
using Core.Utils;

namespace AppClient.Controls
{
    public partial class ucSendMail 
    {
        class SendMailLanguage : ModuleLanguage
        {
            
            public string SendMailLayout { get; set; }
            
            public SendMailLanguage(ModuleInfo moduleInfo)
                : base(moduleInfo)
            {
            }
        }

        private new SendMailLanguage Language
        {
            get
            {
                return (SendMailLanguage)base.Language;
            }
        }

        public override void InitializeLanguage()
        {
            base.Language = new SendMailLanguage(ModuleInfo);

            Language.SendMailLayout = Language.GetLayout("Mail");
            //Language.FormatButton(btnSendMail, "BTN_SENDMAIL");
            base.InitializeLanguage();
        }
    }
}

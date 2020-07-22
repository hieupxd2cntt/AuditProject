﻿using Core.Entities;

namespace AppClient.Controls
{
    public partial class ucShowDataFlow
    {
        class ShowDataFlowLanguage : ModuleLanguage
        {
            public ShowDataFlowLanguage(ModuleInfo moduleInfo)
                : base(moduleInfo)
            {
            }
        }

        public override void InitializeLanguage()
        {
            Language = new ShowDataFlowLanguage(ModuleInfo);
            base.InitializeLanguage();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebCore.Entities;
using WebModelCore;
using WebModelCore.CodeInfo;

namespace WebAppCore.Models
{
    public class ModSearchControlModel
    {
        public ModuleInfo ModInfo { get; set; }
        public ModuleFieldInfo FldInfo { get; set; }
        public List<LanguageInfo> Languages { get; set; }
        public List<CodeInfoModel> CodeInfos { get; set; }
        public List<SelectListItem> Condition { get; set; }
        public int Index { get; set; }
        public ModSearchControlModel()
        {
            ModInfo = new ModuleInfo();
            FldInfo = new ModuleFieldInfo();
            CodeInfos = new List<CodeInfoModel>();
        }
    }
}

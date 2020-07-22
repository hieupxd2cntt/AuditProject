using System.Runtime.Serialization;
using Core.Base;

namespace Core.Entities
{
    [DataContract]
    public class ImportModuleInfo : ModuleInfo
    {
        [DataMember, Column(Name = "IMPORTSTORE")]
        public string ImportStore { get; set; }
        [DataMember, Column(Name = "SELECTSTORE")]
        public string SelectStore { get; set; }
        [DataMember, Column(Name = "EXCELNAME")]
        public string ExcelName { get; set; }
    }
}

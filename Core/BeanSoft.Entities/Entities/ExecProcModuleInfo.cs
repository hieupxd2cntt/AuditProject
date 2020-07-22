using System.Runtime.Serialization;
using Core.Base;

namespace Core.Entities
{
    [DataContract]
    public class ExecProcModuleInfo : ModuleInfo
    {
        [DataMember, Column(Name = "EXECUTESTORE")]
        public string ExecuteStore { get; set; }
    }
}

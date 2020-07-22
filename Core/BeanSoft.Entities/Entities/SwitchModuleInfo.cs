using System.Runtime.Serialization;
using Core.Base;

namespace Core.Entities
{
    [DataContract]
    public class SwitchModuleInfo : ModuleInfo
    {
        [DataMember, Column(Name="SWITCHSTORE")]
        public string SwitchStore { get; set; }
    }
}

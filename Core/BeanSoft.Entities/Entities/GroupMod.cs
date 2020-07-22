using System.Runtime.Serialization;
using Core.Base;

namespace Core.Entities
{
    public class GroupMod {
        [DataMember, Column(Name = "GROUPID")]
        public string GroupId { get; set; }
        [DataMember, Column(Name = "MODID")]
        public string ModId { get; set; }

    }

}

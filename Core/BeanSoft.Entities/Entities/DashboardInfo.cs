using System.Runtime.Serialization;
using Core.Base;

namespace Core.Entities
{
    [DataContract]
    public class DashboardInfo : ModuleInfo
    {
        [DataMember, Column(Name = "ID")]
        public string ID { get; set; }
        [DataMember, Column(Name = "SOURCE")]
        public string Source { get; set; }
        [DataMember, Column(Name = "TITLE")]
        public string Title { get; set; }
        [DataMember, Column(Name = "DESCRIPTION")]
        public string Description { get; set; }
        [DataMember, Column(Name = "AUTOREFRESH")]
        public int Autoupdate { get; set; }
        [DataMember, Column(Name = "LAYOUT")]
        public string Layout { get; set; }
    }
}

using System.Runtime.Serialization;
using Core.Base;
namespace Core.Entities
{
    public class DataClientCache
    {
        [DataMember, Column(Name = "JSONKEY")]
        public string JsonKey { get; set; }
        [DataMember, Column(Name = "PRKEY")]
        public int PrKey { get; set; }
        [DataMember, Column(Name = "JSONDATA")]
        public string JsonData { get; set; }
    }
}

using Core.Base;
using System.Runtime.Serialization;

namespace Core.Entities
{
    [DataContract]
    public class OracleStore : EntityBase
    {
        [DataMember, Column(Name = "STORENAME")]
        public string StoreName { get; set; }
    }
}

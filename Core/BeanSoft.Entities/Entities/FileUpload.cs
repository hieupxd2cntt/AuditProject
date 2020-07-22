using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core.Base;
using System.IO;
using System.ServiceModel;

namespace Core.Entities
{
    [KnownType(typeof(Stream))]
    [MessageContract]
    public class FileUpload
    {
        [MessageHeader]
        public string KeyID { get; set; }
        [MessageHeader]
        public string ModID { get; set; }
        //[MessageHeader]
        //public string Term { get; set; }
        //[MessageHeader]
        //public string TermNo { get; set; }
        //[MessageHeader]
        //public int RYear { get; set; }                
        [MessageHeader]
        public string FileName { get; set; }
        [MessageBodyMember]
        public Stream UploadStream { get; set; }
    }
}

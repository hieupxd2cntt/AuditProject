using System;

namespace Core.Base
{
    public class SyncDBAttribute : Attribute
    {
        public string SyncValue { get; set; }

        public SyncDBAttribute(string name)
        {
            SyncValue = name;
        }
    }
}

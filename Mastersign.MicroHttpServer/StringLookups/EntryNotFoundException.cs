using System;

namespace Mastersign.MicroHttpServer
{
    [Serializable]
    public class EntryNotFoundException
        : Exception
    {
        public string Key { get; }

        public EntryNotFoundException() { }
        
        public EntryNotFoundException(string message, string key)
            : base(message)
        {
            Key = key;
        }

        protected EntryNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

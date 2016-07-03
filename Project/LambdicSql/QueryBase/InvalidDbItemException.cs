using System;
using System.Runtime.Serialization;

namespace LambdicSql.QueryBase
{
    [Serializable]
    public class InvalidDbItemException : Exception
    {
        public InvalidDbItemException() { }
        public InvalidDbItemException(string message) : base(message) { }
        public InvalidDbItemException(string message, Exception innerException) : base(message, innerException) { }
        protected InvalidDbItemException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

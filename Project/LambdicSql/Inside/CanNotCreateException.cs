using System;
using System.Runtime.Serialization;

namespace LambdicSql.Inside
{
    [Serializable]
    public class CanNotCreateException : Exception
    {
        public CanNotCreateException() { }
        public CanNotCreateException(string message) : base(message) { }
        public CanNotCreateException(string message, Exception innerException) : base(message, innerException) { }
        protected CanNotCreateException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
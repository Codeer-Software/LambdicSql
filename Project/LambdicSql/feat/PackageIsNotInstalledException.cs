using System;
using System.Runtime.Serialization;

namespace LambdicSql.feat
{
    [Serializable]
    public class PackageIsNotInstalledException : Exception
    {
        public PackageIsNotInstalledException() { }
        public PackageIsNotInstalledException(string message) : base(message) { }
        public PackageIsNotInstalledException(string message, Exception innerException) : base(message, innerException) { }
        protected PackageIsNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

using System;

namespace LambdicSql.feat
{
    /// <summary>
    /// An exception if the required package is not installed.
    /// </summary>
    public class PackageIsNotInstalledException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PackageIsNotInstalledException() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        public PackageIsNotInstalledException(string message) : base(message) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">InnerException.</param>
        public PackageIsNotInstalledException(string message, Exception innerException) : base(message, innerException) { }
    }
}

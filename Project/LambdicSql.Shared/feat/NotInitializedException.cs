using System;

namespace LambdicSql.feat
{
    /// <summary>
    /// An exception if the required that is not initialized.
    /// </summary>
    public class NotInitializedException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotInitializedException() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        public NotInitializedException(string message) : base(message) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">InnerException.</param>
        public NotInitializedException(string message, Exception innerException) : base(message, innerException) { }
    }
}

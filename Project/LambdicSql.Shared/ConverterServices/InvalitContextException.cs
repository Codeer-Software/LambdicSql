using System;

namespace LambdicSql.ConverterServices
{
    /// <summary>
    /// It is thrown if the method is called with an illegal context.
    /// </summary>
    public class InvalitContextException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public InvalitContextException() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">error method name.</param>
        public InvalitContextException(string name) : base("[" + name + "] can't be called outside lambda.") { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">InnerException.</param>
        public InvalitContextException(string message, Exception innerException) : base(message, innerException) { }
    }
}

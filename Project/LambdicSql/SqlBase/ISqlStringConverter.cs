using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Helper to convert expression to text. 
    /// </summary>
    public interface ISqlStringConverter
    {
        /// <summary>
        /// Context.
        /// </summary>
        SqlConvertingContext Context { get; }

        /// <summary>
        /// Convert object to sql text.
        /// </summary>
        /// <param name="obj">object.</param>
        /// <returns>text.</returns>
        IText ToString(object obj);

        /// <summary>
        /// Get object in expression.
        /// </summary>
        /// <param name="expression">expression.</param>
        /// <returns>object.</returns>
        object ToObject(Expression expression);
    }
}

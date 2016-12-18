using System.Linq.Expressions;
using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Helper to convert expression to text. 
    /// </summary>
    public interface ISqlStringConverter
    {
        /// <summary>
        /// Data base info.
        /// </summary>
        DbInfo DbInfo { get; }

        /// <summary>
        /// Convert object to sql text.
        /// </summary>
        /// <param name="obj">object.</param>
        /// <returns>text.</returns>
        SqlText Convert(object obj);

        /// <summary>
        /// Get object in expression.
        /// </summary>
        /// <param name="expression">expression.</param>
        /// <returns>object.</returns>
        object ToObject(Expression expression);
    }
}

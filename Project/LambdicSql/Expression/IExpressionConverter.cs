using System.Linq.Expressions;
using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.SqlBase
{
    //TODO del
    /// <summary>
    /// Helper to convert expression to text. 
    /// </summary>
    public interface IExpressionConverter
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
        ExpressionElement Convert(object obj);

        /// <summary>
        /// Get object in expression.
        /// </summary>
        /// <param name="expression">expression.</param>
        /// <returns>object.</returns>
        object ToObject(System.Linq.Expressions.Expression expression);
    }
}

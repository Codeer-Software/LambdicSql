using System.Linq.Expressions;
using LambdicSql.SqlBuilder.ExpressionElements;

namespace LambdicSql.ExpressionConverterService
{
    //TODO del
    /// <summary>
    /// Helper to convert expression to text. 
    /// </summary>
    public interface IExpressionConverter
    {
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
        object ToObject(Expression expression);
    }
}

using System.Linq.Expressions;
using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Customize of to convert expression to sql text.
    /// </summary>
    public interface ISqlSyntaxCustomizer
    {
        /// <summary>
        /// Customize of to convert object to sql text.
        /// If you do not customize, return null, and the default process will be executed.
        /// </summary>
        /// <param name="converter">Convertor.</param>
        /// <param name="obj">Object.</param>
        /// <returns>Text.</returns>
        SqlText Convert(ISqlStringConverter converter, object obj);

        /// <summary>
        /// Customize of to convert method to sql text.
        /// If you do not customize, return null, and the default process will be executed.
        /// </summary>
        /// <param name="converter">Convertor.</param>
        /// <param name="expression">Expression.</param>
        /// <returns>Text.</returns>
        SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] expression);

        /// <summary>
        /// Customize of to convert new to sql text.
        /// If you do not customize, return null, and the default process will be executed.
        /// </summary>
        /// <param name="converter">Convertor.</param>
        /// <param name="expression">Expression.</param>
        /// <returns>Text.</returns>
        SqlText Convert(ISqlStringConverter converter, NewExpression expression);

        /// <summary>
        /// Customize of to convert member to sql text.
        /// If you do not customize, return null, and the default process will be executed.
        /// </summary>
        /// <param name="converter">Convertor.</param>
        /// <param name="expression">Expression.</param>
        /// <returns>Text.</returns>
        SqlText Convert(ISqlStringConverter converter, MemberExpression expression);
    }
}

using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.ConverterServices.Inside.CodeParts;
using LambdicSql.Inside.CodeParts;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// StringInterpolator.
    /// </summary>
    /// <typeparam name="T">DB's type.</typeparam>
    public class StringInterpolator<T> where T : class
    {
        /// <summary>
        /// Create sql from FormattableString.
        /// </summary>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql Sql(Expression<Func<T, FormattableString>> expression)
            => Inside.SqlBuilder.FromExpressionContainFormattableString<T>(expression.Body);

        /// <summary>
        /// Create sql from FormattableString.
        /// </summary>
        /// <typeparam name="TResult">The type represented by expression.</typeparam>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql<TResult> Sql<TResult>(Expression<Func<T, FormattableString>> expression)
            => Inside.SqlBuilder.FromExpressionContainFormattableString<T, TResult>(expression.Body);
    }

    /// <summary>
    /// StringInterpolator.
    /// </summary>
    public class StringInterpolator
    {
        /// <summary>
        /// Create sql from FormattableString.
        /// </summary>
        /// <param name="formattableString">formattable string.</param>
        /// <returns>Sql.</returns>
        public static Sql Sql(FormattableString formattableString)
            => Inside.SqlBuilder.FromFormattableString(formattableString.Format, formattableString.GetArguments());

        /// <summary>
        /// Create sql from FormattableString.
        /// </summary>
        /// <typeparam name="TResult">The type represented by expression.</typeparam>
        /// <param name="formattableString">formattable string.</param>
        /// <returns>Sql.</returns>
        public static Sql<TResult> Sql<TResult>(FormattableString formattableString)
            => Inside.SqlBuilder.FromFormattableString<TResult>(formattableString.Format, formattableString.GetArguments());

        /// <summary>
        /// Create sql from FormattableString.
        /// </summary>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql Sql(Expression<Func<FormattableString>> expression)
            => Inside.SqlBuilder.FromExpressionContainFormattableString(expression.Body);

        /// <summary>
        /// Create sql from FormattableString.
        /// </summary>
        /// <typeparam name="TResult">The type represented by expression.</typeparam>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql<TResult> Sql<TResult>(Expression<Func<FormattableString>> expression)
            => Inside.SqlBuilder.FromExpressionContainFormattableString<Non, TResult>(expression.Body);
    }
}

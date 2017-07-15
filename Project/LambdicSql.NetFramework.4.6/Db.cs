using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.Inside.CodeParts;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public partial class Db<T> where T : class
    {
        /// <summary>
        /// Create sql from FormattableString.
        /// </summary>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql InterpolateSql(Expression<Func<T, FormattableString>> expression)
            => new Sql(MakeCode(expression.Body));

        /// <summary>
        /// Create sql from FormattableString.
        /// </summary>
        /// <typeparam name="TResult">The type represented by expression.</typeparam>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql<TResult> InterpolateSql<TResult>(Expression<Func<T, FormattableString>> expression)
            => new Sql<TResult>(MakeCode(expression.Body));

        static ICode MakeCode(Expression exp)
        {
            var methodExp = exp as MethodCallExpression;
            var db = DBDefineAnalyzer.GetDbInfo<T>();
            var converter = new ExpressionConverter(db);
            var obj = converter.ConvertToObject(methodExp.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = methodExp.Arguments[1] as NewArrayExpression;
            return new StringFormatCode(text, array.Expressions.Select(e => converter.ConvertToCode(e)).ToArray());
        }
    }

    /// <summary>
    /// DB.
    /// </summary>
    public partial class Db
    {
        /// <summary>
        /// Create sql from FormattableString.
        /// </summary>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql InterpolateSql(Expression<Func<FormattableString>> expression)
            => new Sql(MakeCode(expression.Body));

        /// <summary>
        /// Create sql from FormattableString.
        /// </summary>
        /// <typeparam name="TResult">The type represented by expression.</typeparam>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql<TResult> InterpolateSql<TResult>(Expression<Func<FormattableString>> expression)
            => new Sql<TResult>(MakeCode(expression.Body));

        static ICode MakeCode(Expression exp)
        {
            var methodExp = exp as MethodCallExpression;
            var db = DBDefineAnalyzer.GetDbInfo<Non>();
            var converter = new ExpressionConverter(db);
            var obj = converter.ConvertToObject(methodExp.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = methodExp.Arguments[1] as NewArrayExpression;
            return new StringFormatCode(text, array.Expressions.Select(e => converter.ConvertToCode(e)).ToArray());
        }
    }
}

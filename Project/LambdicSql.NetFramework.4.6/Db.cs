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
            var text = (string)obj;
            var array = methodExp.Arguments[1] as NewArrayExpression;

            var args = new ICode[array.Expressions.Count];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = converter.ConvertToCode(array.Expressions[i]);
            }
            return new StringFormatCode(text, args);
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
        /// <param name="formattableString">formattable string.</param>
        /// <returns>Sql.</returns>
        public static Sql InterpolateSql(FormattableString formattableString)
            => new Sql(MakeCode(formattableString));

        /// <summary>
        /// Create sql from FormattableString.
        /// </summary>
        /// <typeparam name="TResult">The type represented by expression.</typeparam>
        /// <param name="formattableString">formattable string.</param>
        /// <returns>Sql.</returns>
        public static Sql<TResult> InterpolateSql<TResult>(FormattableString formattableString)
            => new Sql<TResult>(MakeCode(formattableString));

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

        static ICode MakeCode(FormattableString formattableString)
        {
            var argsObj = formattableString.GetArguments();
            var args = new ICode[argsObj.Length];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = new ParameterCode(argsObj[i]);
            }
            return new StringFormatCode(formattableString.Format, args);
        }

        static ICode MakeCode(Expression exp)
        {
            var methodExp = exp as MethodCallExpression;
            var db = DBDefineAnalyzer.GetDbInfo<Non>();
            var converter = new ExpressionConverter(db);
            var obj = converter.ConvertToObject(methodExp.Arguments[0]);
            var text = (string)obj;
            var array = methodExp.Arguments[1] as NewArrayExpression;

            var args = new ICode[array.Expressions.Count];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = converter.ConvertToCode(array.Expressions[i]);
            }
            return new StringFormatCode(text, array.Expressions.Select(e => converter.ConvertToCode(e)).ToArray());
        }
    }
}

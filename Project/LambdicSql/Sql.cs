using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Syntaxes.Inside;
using System;
using System.Linq.Expressions;
using LambdicSql.BuilderServices.Syntaxes;

namespace LambdicSql
{
    /// <summary>
    /// LambdicSql's query creator.
    /// </summary>
    /// <typeparam name="TDB">DB's type.</typeparam>
    public class Sql<TDB> where TDB : class
    {
        /// <summary>
        /// Create an expression that will be part of the query.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">An expression expressing a part of query by C #.</param>
        /// <returns>An expression that will be part of the query.</returns>
        public static SqlExpression<TResult> Of<TResult>(Expression<Func<TDB, TResult>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<TDB>();
            return new SqlExpression<TResult>(MakeSynatx(db, expression.Body));
        }

        /// <summary>
        /// Create a query.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="expression">An expression expressing a query by C #.</param>
        /// <returns>A query.</returns>
        public static SqlExpression<TSelected> Of<TSelected>(Expression<Func<TDB, ClauseChain<TSelected>>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<TDB>();
            return new SqlExpression<TSelected>(MakeSynatx(db, expression.Body));
        }

        /// <summary>
        /// Create a query.
        /// Add name.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">An expression expressing a query by C #.</param>
        /// <returns>A query.</returns>
        public static SqlExpression<TResult> Of<TResult>(Expression<Func<TDB, SqlExpression<TResult>>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<TDB>();
            var core = expression.Body as MemberExpression;
            return new SqlExpression<TResult>(new AliasSyntax(core.Member.Name));
        }

        /// <summary>
        /// Create a query.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">An expression expressing a query by C #.</param>
        /// <returns>A query.</returns>
        public static SqlRecursiveArgumentsExpression<TResult> Of<TResult>(Expression<Func<TDB, Keywords.RecursiveArguments<TResult>>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<TDB>();
            return new SqlRecursiveArgumentsExpression<TResult>(MakeSynatx(db, expression.Body));
        }

        static Syntax MakeSynatx(DbInfo dbInfo, Expression core)
        {
            var converter = new ExpressionConverter(dbInfo);
            return core == null ? string.Empty : converter.Convert(core);
        }
    }
}

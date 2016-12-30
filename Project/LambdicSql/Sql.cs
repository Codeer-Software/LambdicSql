using LambdicSql.ExpressionConverterService;
using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBuilder;
using System;
using System.Linq.Expressions;

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
            return new SqlExpressionSingle<TResult>(db, expression.Body);
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
            return new SqlExpressionSingle<TSelected>(db, expression.Body);
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
            return new SqlExpressionSingle<TResult>(db, new AliasText(core.Member.Name));
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
            return new SqlRecursiveArgumentsExpression<TResult>(db, expression.Body);
        }
    }
}

using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq;
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
        public static SqlExpression<TResult> Create<TResult>(Expression<Func<TDB, TResult>> expression)
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
        public static SqlQuery<TSelected> Create<TSelected>(Expression<Func<TDB, IClauseChain<TSelected>>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<TDB>();
            return new SqlQuery<TSelected>(new SqlExpressionSingle<IClauseChain<TSelected>>(db, expression.Body));
        }

        /// <summary>
        /// Create a query.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">An expression expressing a query by C #.</param>
        /// <returns>A query.</returns>
        public static SqlExpression<TResult> Create<TResult>(Expression<Func<TDB, SqlExpression<TResult>>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<TDB>();
            var core = expression.Body as MemberExpression;
            return new SqlExpressionSingle<TResult>(db, new AliasText(core.Member.Name));
        }

        /// <summary>
        /// Create a query.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="expression">An expression expressing a query by C #.</param>
        /// <returns>A query.</returns>
        public static SqlQuery<TSelected> Create<TSelected>(Expression<Func<TDB, SqlQuery<TSelected>>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<TDB>();
            var core = expression.Body as MemberExpression;
            return new SqlQuery<TSelected>(new SqlExpressionSingle<IClauseChain<TSelected>>(db, new AliasText(core.Member.Name)));
        }
    }


    //TODO
    class AliasText : SingleText
    {
        public AliasText(string text) : base(text) { }
    }
}

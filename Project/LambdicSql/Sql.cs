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
        public static SqlExpression<TSelected> Create<TSelected>(Expression<Func<TDB, ClauseChain<TSelected>>> expression)
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
        public static SqlExpression<TResult> Create<TResult>(Expression<Func<TDB, SqlExpression<TResult>>> expression)
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
        public static SqlRecursiveArgumentsExpression<TResult> Create<TResult>(Expression<Func<TDB, Keywords.RecursiveArguments<TResult>>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<TDB>();
            return new SqlRecursiveArgumentsExpression<TResult>(db, expression.Body);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSelected"></typeparam>
    public class SqlRecursiveArgumentsExpression<TSelected> : SqlExpression<TSelected>
    {
        /// <summary>
        /// 
        /// </summary>
        public override DbInfo DbInfo { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public override ExpressionElement ExpressionElement { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbInfo"></param>
        /// <param name="core"></param>
        public SqlRecursiveArgumentsExpression(DbInfo dbInfo, Expression core)
        {
            DbInfo = dbInfo;
            var converter = new LambdicSql.Inside.ExpressionConverter(dbInfo);
            if (core == null) ExpressionElement = string.Empty;
            else ExpressionElement = converter.Convert(core);
        }
    }

    //TODO
    class AliasText : SingleTextElement
    {
        public AliasText(string text) : base(text) { }
    }
}

using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    //TODO delete new() 
    public class Sql<TDB> where TDB : class, new()
    {
        public static SqlExpression<TResult> Create<TResult>(Expression<Func<TDB, TResult>> exp)
        {
            var db = DBDefineAnalyzer.GetDbInfo(() => new TDB());
            return new SqlExpressionSingle<TResult>(db, exp.Body);
        }

        public static SqlQuery<TSelected> Create<TSelected>(Expression<Func<TDB, IQuery<TSelected>>> exp)
        {
            var db = DBDefineAnalyzer.GetDbInfo(() => new TDB());
            return new SqlQuery<TSelected>(new SqlExpressionSingle<IQuery<TSelected>>(db, exp.Body));
        }

        public static SqlQuery<TSelected> Create<TSelected, TInfo>(Expression<Func<TDB, IQuery<TSelected, TInfo>>> exp)
        {
            var db = DBDefineAnalyzer.GetDbInfo(() => new TDB());
            return new SqlQuery<TSelected>(new SqlExpressionSingle<IQuery<TSelected, TInfo>>(db, exp.Body));
        }
    }
}

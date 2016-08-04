using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public class Sql<TDB> where TDB : class, new()
    {
        public static ISqlExpression<TResult> Create<TResult>(Expression<Func<TDB, ISqlKeyWord<Non>, TResult>> exp)
        {
            var db = DBDefineAnalyzer.GetDbInfo(() => new TDB());
            return new SqlExpression<TResult>(db, exp.Body);
        }
    }


    public static class EX
    {
        public static ISqlFunc Func<T>(this ISqlKeyWord<T> words) => null;
        public static ISqlUtility Util<T>(this ISqlKeyWord<T> words) => null;
        public static IWindowFuncs Window<T>(this ISqlKeyWord<T> words) => null;
        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            return string.Empty;
        }
    }
}

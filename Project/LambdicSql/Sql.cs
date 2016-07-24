using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class Sql
    {
        public static Action<string> Log { get; set; }
        public static ISqlFuncs Funcs { get; set; }

        public static IQuery<T, T> Query<T>(Expression<Func<T>> define) where T : class
            => DBDefineAnalyzer.CreateQuery(define);

        public static IQuery<T, T> Query<T>() where T : class, new()
            => DBDefineAnalyzer.CreateQuery(()=>new T());
    }
}

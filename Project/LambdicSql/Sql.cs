using LambdicSql.Inside;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class Sql
    {
        public static Action<string> Log { get; set; }
        public static IQueryStart<T, T> Using<T>(Expression<Func<T>> define) where T : class
            => DBDefineAnalyzer.CreateQuery(define);
        public static IQueryStart<T, T> Using<T>() where T : class, new()
            => DBDefineAnalyzer.CreateQuery(()=>new T());
    }
}

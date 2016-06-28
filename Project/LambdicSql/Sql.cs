using LambdicSql.Inside;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{

    //TODO No! using is keyword.→Reference?
    public static class Sql
    {
        public static Action<string> Log { get; set; }
        public static IQuery<T, T> Using<T>(Expression<Func<T>> define) where T : class
            => DBDefineAnalyzer.CreateQuery(define);
        public static IQuery<T, T> Using<T>() where T : class, new()
            => DBDefineAnalyzer.CreateQuery(()=>new T());
    }
}

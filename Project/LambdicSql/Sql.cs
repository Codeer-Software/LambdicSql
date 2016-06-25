using LambdicSql.Inside;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class Sql
    {
        public static IQueryStart<T, T> Using<T>(Expression<Func<T>> define) where T : class
            => DBDefineAnalyzer.CreateQuery(define);
    }
}

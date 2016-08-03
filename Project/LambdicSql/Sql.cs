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
        public static ISqlWords Words { get; set; }
        public static IWindowWords Window { get; set; }
        public static IQuery<T, T> Query<T>(Expression<Func<T>> define) where T : class
            => DBDefineAnalyzer.CreateQuery(define);

        public static IQuery<T, T> Query<T>() where T : class, new()
            => DBDefineAnalyzer.CreateQuery(() => new T());


        //TODO
        public static Creating<T> Using<T>() where T : class, new()
            => new Creating<T>() { Query = DBDefineAnalyzer.CreateQuery(() => new T()) };

        public static Creating<T> Using<T>(Expression<Func<T>> define) where T : class, new()
            => new Creating<T>() { Query = DBDefineAnalyzer.CreateQuery(define) };
    }

    public class Creating<T> where T : class, new()
    {
        public IQuery<T, T> Query { get; set; }
    }

    public class Sql<TDB>
        where TDB : class, new()
    {
        //TODO
        public static ISqlExpression<TDB, TResult> Create<TResult>(Expression<Func<TDB, IQueryDesigner<NoSelected>, TResult>> exp)
        {
            var query = DBDefineAnalyzer.CreateQuery(() => new TDB());
            return new SqlExpressionCore<TDB, TResult>(query, exp.Body);
        }
    }
}

using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class SqlExpressionExtensions
    {
        public static DB Cast<DB>(this ISqlExpression query)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }
        /*
        public static DB Cast<DB, TResult>(this ISqlExpression<DB, TResult> query)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }*/
        public static TResult Cast<DB, TResult>(this ISqlExpression<DB, ISqlWords<TResult>> query)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }

        public static ISqlExpression<TDB, TResult> Expression<TDB, TResult>(this IQuery<TDB> query, Expression<Func<TDB, TResult>> exp)
            where TDB : class
            => new SqlExpressionCore<TDB, TResult>(query, exp.Body);

        public static SqlInfo ToSqlInfo<T>(this ISqlExpression exp)
             where T : IDbConnection
        {
            var parameters = new PrepareParameters();
            var converter = new SqlStringConverter(exp.Query.Db, parameters, QueryCustomizeResolver.CreateCustomizer(typeof(T).FullName), 0);
            return new SqlInfo(exp.ToString(converter), parameters.GetParameters());
        }

        //TODO
        public static ISqlExpression<TDB, TResult> Create<TDB, TResult>(this Creating<TDB> query, Expression<Func<TDB, IQueryDesigner<NoSelected>, TResult>> exp)
            where TDB : class, new()
            => new SqlExpressionCore<TDB, TResult>(query.Query, exp.Body);

        public static ISqlExecutor<TSelected> ToExecutor<TDB, TSelected>(this ISqlExpression<TDB, ISqlWords<TSelected>> exp, IDbConnection connection)
            where TDB : class
            where TSelected : class
            => new SqlExecutor<TSelected>(connection, new XXX<TSelected>(exp.Query.Db, exp));






        public static ISqlExpression<TDB, TResult> Concat<TDB, TResult>(this ISqlExpression<TDB, TResult> query, ISqlExpression addExp)
            where TDB : class
          => new SqlExpressionCore<TDB, TResult>((SqlExpressionCore<TDB, TResult>)query, addExp.Expression);
    }

    public interface ISqlHelper : ISqlWords { }
    public interface IQueryDesigner<T> : ISqlWords<T>, ISqlFuncs, IWindowWords, ISqlHelper { }

    public class NoSelected { }
    public class XXXClause : IClause
    {
        ISqlExpression _exp;
        public IClause Clone() => this;
        public string ToString(ISqlStringConverter decoder) => _exp.ToString(decoder);
        public XXXClause(ISqlExpression exp)
        {
            _exp = exp;
        }
    }

    public class XXX<TSelect> : ISelectedQuery<TSelect>
        where TSelect : class
    {
        public ISqlExpression exp;
        public DbInfo Db { get; }
        public IClause[] GetClausesClone() => new IClause[] { c };
        public Func<Func<ISqlResult, TSelect>> Create
        {
            get
            {
                var indexInSelect = Db.SelectClause.GetElements().Select(e => e.Name).ToList();
                return () => ExpressionToCreateFunc.ToCreateUseDbResult<TSelect>(indexInSelect, Db.SelectClause.Define);
            }
        }
        IClause c;
        public XXX(DbInfo db, ISqlExpression exp)
        {
            Db = db;
            c = new XXXClause(exp);
        }
    }

}

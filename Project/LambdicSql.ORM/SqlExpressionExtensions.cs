using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ORM
{
    public static class SqlExpressionExtensions
    {
        public static ISqlExecutor<TSelected> ToExecutor<TDB, TSelected>(this ISqlExpression<TDB, ISqlWords<TSelected>> exp, IDbConnection connection)
            where TDB : class
            where TSelected : class
            => new SqlExecutor<TSelected>(connection, new XXX<TSelected>(exp.DbInfo, exp));
    }
    
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

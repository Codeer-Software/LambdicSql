using LambdicSql.Clause.From;
using LambdicSql.Clause.GroupBy;
using LambdicSql.Clause.Having;
using LambdicSql.Clause.OrderBy;
using LambdicSql.Clause.Select;
using LambdicSql.Clause.Where;
using System;
using System.Linq;

namespace LambdicSql.QueryBase
{
    public interface IQuery
    {
        DbInfo Db { get; }
        IClause[] GetClausesClone();


        SelectClause Select { get; }
        FromClause From { get; }
        WhereClause Where { get; }
        GroupByClause GroupBy { get; }
        HavingClause Having { get; }
        OrderByClause OrderBy { get; }

    }

    public interface IQuery<TSelect> : IQuery
        where TSelect : class
    {
        Func<IDbResult, TSelect> Create { get; }
    }

    public interface IQuery<TDB, TSelect> : IQuery<TSelect>
        where TDB : class
        where TSelect : class
    {
    }


    public interface IQuery<TDB, TSelect, TClause> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
        where TClause : IClause
    {
        IClause Clause { get; }
    }


    public class ClauseMakingQuery<TDB, TSelect, TClause> : IQuery<TDB, TSelect, TClause>
        where TDB : class
        where TSelect : class
        where TClause : IClause
    {        IClause[] _clause;

        public Func<IDbResult, TSelect> Create { get; }
        public DbInfo Db { get; }
        public IClause Clause => _clause.Length == 0 ? null : _clause[_clause.Length - 1];
        public IClause[] GetClausesClone() => _clause.Select(e => e.Clone()).ToArray();

        public ClauseMakingQuery(IQuery<TDB, TSelect, TClause> src)
        {
            Db = src.Db;
            Create = src.Create;
            _clause = src.GetClausesClone();
        }

        //////////@@@

        public SelectClause Select => null;
        public FromClause From => null;
        public WhereClause Where => null;
        public GroupByClause GroupBy => null;
        public HavingClause Having => null;
        public OrderByClause OrderBy => null;
    }
}


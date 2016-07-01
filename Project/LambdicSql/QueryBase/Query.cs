using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.Inside
{
    public class Query<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    {
        IEnumerable<IClause> _clauses;

        public Func<IDbResult, TSelect> Create { get; }
        public DbInfo Db { get; }
        public IClause[] GetClausesClone() => _clauses.Select(e => e.Clone()).ToArray();

        internal Query()
        {
            _clauses = new IClause[0];
        }

        internal Query(DbInfo db, Func<IDbResult, TSelect> create)
        {
            Db = db;
            Create = create;
            _clauses = new IClause[0];
        }

        public Query(IQuery<TDB, TSelect> src, IClause newClause)
        {
            Db = src.Db;
            Create = src.Create;
            _clauses = src.GetClausesClone().Concat(new[] { newClause }).ToArray();
        }
    }
}

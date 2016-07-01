using System;
using System.Linq;

namespace LambdicSql.QueryBase
{
    public class ClauseMakingQuery<TDB, TSelect, TClause> : IQuery<TDB, TSelect, TClause>
        where TDB : class
        where TSelect : class
        where TClause : IClause
    {
        IClause[] _clauses;

        public DbInfo Db { get; }
        public Func<IDbResult, TSelect> Create { get; }
        public IClause[] GetClausesClone() => _clauses.Select(e => e.Clone()).ToArray();

        public ClauseMakingQuery(IQuery<TDB, TSelect> src, TClause newClause)
        {
            Db = src.Db;
            Create = src.Create;
            _clauses = src.GetClausesClone().Concat(new IClause[] { newClause }).ToArray();
        }

        public IQuery<TDB, TSelect, TClause> CustomClone(Action<TClause> custom)
        {
            var clauses = GetClausesClone();
            var clone = (TClause)clauses[_clauses.Length - 1];
            custom(clone);
            return new ClauseMakingQuery<TDB, TSelect, TClause>(Db, Create, clauses);
        }

        public ClauseMakingQuery(DbInfo db, Func<IDbResult, TSelect> create, IClause[] clauses)
        {
            Db = db;
            Create = create;
            _clauses = clauses;
        }
    }
}

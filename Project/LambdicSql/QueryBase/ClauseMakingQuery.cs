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
        public Func<Func<ISqlResult, TSelect>> Create { get; }
        public IClause[] GetClausesClone() => _clauses.Select(e => e.Clone()).ToArray();

        public ClauseMakingQuery(IQuery<TDB, TSelect> src, params TClause[] newClause)
        {
            Db = src.Db;
            Create = src.Create;
            _clauses = src.GetClausesClone().Concat(newClause.Select(e=>(IClause)e)).ToArray();
        }

        public ClauseMakingQuery(DbInfo db, Func<Func<ISqlResult, TSelect>> create, IClause[] clauses)
        {
            Db = db;
            Create = create;
            _clauses = clauses;
        }

        public IQuery<TDB, TSelect, TClause> CustomClone(Action<TClause> custom)
        {
            var clauses = GetClausesClone();
            if (0 < _clauses.Length)
            {
                var clone = (TClause)clauses[_clauses.Length - 1];
                custom(clone);
            }
            return new ClauseMakingQuery<TDB, TSelect, TClause>(Db, Create, clauses);
        }
    }
}

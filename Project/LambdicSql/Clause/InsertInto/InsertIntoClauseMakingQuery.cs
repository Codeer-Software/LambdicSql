using LambdicSql.QueryBase;
using System;
using System.Linq;

namespace LambdicSql.Clause.InsertInto
{

    public class InsertIntoClauseMakingQuery<TDB, TTable> : IInsertIntoQuery<TDB, TTable>
    where TDB : class
    where TTable : class
    {
        IClause[] _clauses;

        public DbInfo Db { get; }
        public Func<ISqlResult, TDB> Create { get; }
        public IClause[] GetClausesClone() => _clauses.Select(e => e.Clone()).ToArray();

        public TTable GetTable(TDB db)
            => ((InsertIntoClause<TDB, TTable>)_clauses[_clauses.Length - 1]).GetTableFunc(db);

        public InsertIntoClauseMakingQuery(IQuery<TDB, TDB> src, InsertIntoClause<TDB, TTable> newClause)
        {
            Db = src.Db;
            Create = src.Create;
            _clauses = src.GetClausesClone().Concat(new IClause[] { newClause }).ToArray();
        }

        public IQuery<TDB, TDB, InsertIntoClause<TDB, TTable>> CustomClone(Action<InsertIntoClause<TDB, TTable>> custom)
        {
            var clauses = GetClausesClone();
            if (0 < _clauses.Length)
            {
                var clone = (InsertIntoClause<TDB, TTable>)clauses[_clauses.Length - 1];
                custom(clone);
            }
            return new InsertIntoClauseMakingQuery<TDB, TTable>(Db, Create, clauses);
        }

        public InsertIntoClauseMakingQuery(DbInfo db, Func<ISqlResult, TDB> create, IClause[] clauses)
        {
            Db = db;
            Create = create;
            _clauses = clauses;
        }
    }
}

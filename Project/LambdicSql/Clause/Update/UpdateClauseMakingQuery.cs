using LambdicSql.QueryBase;
using System;
using System.Linq;

namespace LambdicSql.Clause.Update
{
    public class UpdateClauseMakingQuery<TDB, TTable> : IUpdateQuery<TDB, TTable>, ISetQuery<TDB, TTable>
        where TDB : class
        where TTable : class
    {
        IClause[] _clauses;

        public DbInfo Db { get; }
        public Func<Func<ISqlResult, TDB>> Create { get; }
        public IClause[] GetClausesClone() => _clauses.Select(e => e.Clone()).ToArray();

        public UpdateClauseMakingQuery(IQuery<TDB, TDB> src, UpdateClause newClause)
        {
            Db = src.Db;
            Create = src.Create;
            _clauses = src.GetClausesClone().Concat(new IClause[] { newClause }).ToArray();
        }

        public IQuery<TDB, TDB, UpdateClause> CustomClone(Action<UpdateClause> custom)
        {
            var clauses = GetClausesClone();
            if (0 < _clauses.Length)
            {
                var clone = (UpdateClause)clauses[_clauses.Length - 1];
                custom(clone);
            }
            return new UpdateClauseMakingQuery<TDB, TTable>(Db, Create, clauses);
        }

        public UpdateClauseMakingQuery(DbInfo db, Func<Func<ISqlResult, TDB>> create, IClause[] clauses)
        {
            Db = db;
            Create = create;
            _clauses = clauses;
        }
    }
}

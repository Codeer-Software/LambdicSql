using System;
using System.Linq.Expressions;
using LambdicSql.QueryBase;
using System.Linq;
using System.Collections.Generic;

namespace LambdicSql.Clause.Update
{
    //TODO refactoring.
    public class UpdateClause : IClause
    {
        List<SetElement> Elements { get; set; } = new List<SetElement>();
        Expression SelectDb { get; }
        public UpdateClause(Expression db)
        {
            SelectDb = db;
        }

        public IClause Clone() => new UpdateClause(SelectDb) { Elements = Elements.ToList() };
        public string ToString(ISqlStringConverter decoder) =>
            "UPDATE " + decoder.ToString(SelectDb) + Environment.NewLine +
            "SET" + Environment.NewLine + "\t" +
            string.Join("," + Environment.NewLine + "\t", Elements.Select(e=>ToString(e, decoder)).ToArray());

        static string ToString(SetElement element, ISqlStringConverter decoder)
            => decoder.ToString(element.SelectData) + " = " + decoder.ToString(element.Value);

        internal void Set(Expression selectData, object value)
            => Elements.Add(new SetElement(selectData, value));
    }

    public class SetElement
    {
        public Expression SelectData { get; }
        public object Value { get; }
        public SetElement(Expression selectData, object value)
        {
            SelectData = selectData;
            Value = value;
        }
    }
    
    public interface IUpdateQuery<TDB, TTable> : IQuery<TDB, TDB, UpdateClause>
            where TDB : class
            where TTable : class
    { }

    public class UpdateClauseMakingQuery<TDB, TTable> : IUpdateQuery<TDB, TTable>
    where TDB : class
    where TTable : class
    {
        IClause[] _clauses;

        public DbInfo Db { get; }
        public Func<ISqlResult, TDB> Create { get; }
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

        public UpdateClauseMakingQuery(DbInfo db, Func<ISqlResult, TDB> create, IClause[] clauses)
        {
            Db = db;
            Create = create;
            _clauses = clauses;
        }
    }
}

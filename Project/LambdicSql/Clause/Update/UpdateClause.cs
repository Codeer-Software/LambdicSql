using System;
using System.Linq.Expressions;
using LambdicSql.QueryBase;
using System.Linq;
using System.Collections.Generic;

namespace LambdicSql.Clause.Update
{
    public class UpdateClause : IClause
    {
        List<SetElement> _elements = new List<SetElement>();
        Expression _selectDb;

        public UpdateClause(Expression db)
        {
            _selectDb = db;
        }

        public IClause Clone() => new UpdateClause(_selectDb) { _elements = _elements.ToList() };
        public string ToString(ISqlStringConverter decoder) =>
            "UPDATE " + decoder.ToString(_selectDb) + Environment.NewLine +
            "SET" + Environment.NewLine + "\t" +
            string.Join("," + Environment.NewLine + "\t", _elements.Select(e=>ToString(e, decoder)).ToArray());

        static string ToString(SetElement element, ISqlStringConverter decoder)
            => decoder.ToString(element.SelectData) + " = " + decoder.ToString(element.Value);

        internal void Set(Expression selectData, object value)
            => _elements.Add(new SetElement(selectData, value));
    }
}

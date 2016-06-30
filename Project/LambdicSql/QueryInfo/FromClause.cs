using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class FromClause
    {
        List<JoinClause> _join = new List<JoinClause>();

        public string MainTableSqlFullName { get; }
        public Expression MainTable { get; }
        public JoinClause[] GetJoins() => _join.ToArray();

        public FromClause(string mainTableSqlFullName)
        {
            MainTableSqlFullName = mainTableSqlFullName;
        }

        public FromClause(Expression mainTable)
        {
            MainTable = mainTable;
        }

        public FromClause Clone()
        {
            var clone = new FromClause(MainTable);
            clone._join.AddRange(_join);
            return clone;
        }

        internal void Join(JoinClause join) => _join.Add(join);
    }
}

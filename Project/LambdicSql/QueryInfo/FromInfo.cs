using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class FromInfo
    {
        List<JoinInfo> _join = new List<JoinInfo>();

        public string MainTableSqlFullName { get; }
        public Expression MainTable { get; }
        public JoinInfo[] GetJoins() => _join.ToArray();

        public FromInfo(string mainTableSqlFullName)
        {
            MainTableSqlFullName = mainTableSqlFullName;
        }

        public FromInfo(Expression mainTable)
        {
            MainTable = mainTable;
        }

        public FromInfo Clone()
        {
            var clone = new FromInfo(MainTable);
            clone._join.AddRange(_join);
            return clone;
        }

        internal void Join(JoinInfo join) => _join.Add(join);
    }
}

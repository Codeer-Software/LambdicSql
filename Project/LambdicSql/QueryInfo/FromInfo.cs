using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class FromInfo
    {
        List<JoinInfo> _join = new List<JoinInfo>();

        public TableInfo MainTable { get; }
        public JoinInfo[] GetJoins() => _join.ToArray();

        public FromInfo(TableInfo mainTable)
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

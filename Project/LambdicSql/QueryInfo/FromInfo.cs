using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class FromInfo
    {
        List<JoinInfo> _join = new List<JoinInfo>();

        public TableInfo MainTable { get; }
        public IReadOnlyList<JoinInfo> Joins => _join;

        public FromInfo(TableInfo mainTable)
        {
            MainTable = mainTable;
        }
        
        public FromInfo Clone()
        {
            var clone = new FromInfo(MainTable);
            clone._join.AddRange(Joins);
            return clone;
        }

        internal void Join(JoinInfo join) => _join.Add(join);
    }
}

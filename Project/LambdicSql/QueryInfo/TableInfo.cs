using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.QueryInfo
{
    public class TableInfo
    {
        Dictionary<string, ColumnInfo> _children = new Dictionary<string, ColumnInfo>();

        public IReadOnlyList<string> FullName { get; }
        public string FullNameText => string.Join(".", FullName);
        public string Name => FullName[FullName.Count - 1];
        public Dictionary<string, ColumnInfo> Children => _children;

        public TableInfo(string[] name)
        {
            FullName = name.Where(e => !string.IsNullOrEmpty(e)).ToList();
        }

        internal void Add(ColumnInfo column)
        {
            if (!Children.ContainsKey(column.Name))
            {
                Children.Add(column.Name, column);
            }
        }
    }
}

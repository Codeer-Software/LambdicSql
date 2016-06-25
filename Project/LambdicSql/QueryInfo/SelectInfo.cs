using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class SelectInfo
    {
        Dictionary<string, SelectElementInfo> _aliasElements = new Dictionary<string, SelectElementInfo>();
        Dictionary<string, ColumnInfo> _aliasColumns = new Dictionary<string, ColumnInfo>();

        public IReadOnlyDictionary<string, SelectElementInfo> AliasElements => _aliasElements;

        //TODO no need.
        public IReadOnlyDictionary<string, ColumnInfo> AliasColumns => _aliasColumns;
        public IReadOnlyDictionary<string, ColumnInfo> DbColumns { get; }

        public SelectInfo(IReadOnlyDictionary<string, ColumnInfo> dbColumns)
        {
            DbColumns = dbColumns;
        }

        internal void Add(string name, SelectElementInfo selectElementInfo, ColumnInfo columnInfo)
        {
            _aliasElements.Add(name, selectElementInfo);
            _aliasColumns.Add(name, columnInfo);
        }
    }
}

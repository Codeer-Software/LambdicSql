using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.QueryInfo
{
    public class DbInfo
    {
        Dictionary<string, ColumnInfo> _lambdaNameAndColumn = new Dictionary<string, ColumnInfo>();
        Dictionary<string, TableInfo> _lambdaNameAndTable = new Dictionary<string, TableInfo>();
        public IReadOnlyDictionary<string, ColumnInfo> LambdaNameAndColumn => _lambdaNameAndColumn;
        public IReadOnlyDictionary<string, TableInfo> LambdaNameAndTable => _lambdaNameAndTable;

        public void Add(ColumnInfo col)
        {
            _lambdaNameAndColumn.Add(col.LambdaFullName, col);

            var sep = col.LambdaFullName.Split('.');
            var tableLambda = string.Join(".", sep.Take(sep.Length - 1));
            if (!_lambdaNameAndTable.ContainsKey(tableLambda))
            {
                sep = col.SqlFullName.Split('.');
                var tableSql = string.Join(".", sep.Take(sep.Length - 1));
                _lambdaNameAndTable.Add(tableLambda, new TableInfo(tableLambda, tableSql));
            }
        }
    }
}

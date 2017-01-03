using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.ConverterServices
{
    /// <summary>
    /// Data Base info.
    /// </summary>
    public class DbInfo
    {
        Dictionary<string, ColumnInfo> _lambdaNameAndColumn = new Dictionary<string, ColumnInfo>();
        Dictionary<string, TableInfo> _lambdaNameAndTable = new Dictionary<string, TableInfo>();

        /// <summary>
        /// Get dictionary of full name in lambda and column info.
        /// </summary>
        /// <returns>dictionary of full name in lambda and column info.</returns>
        public Dictionary<string, ColumnInfo> GetLambdaNameAndColumn() => _lambdaNameAndColumn.ToDictionary(e=>e.Key, e=>e.Value);

        /// <summary>
        /// Get dictionary of full name in lambda and table info.
        /// </summary>
        /// <returns>dictionary of full name in lambda and table info.</returns>
        public Dictionary<string, TableInfo> GetLambdaNameAndTable() => _lambdaNameAndTable.ToDictionary(e => e.Key, e => e.Value);

        internal void Add(ColumnInfo col)
        {
            _lambdaNameAndColumn.Add(col.LambdaFullName, col);

            var sep = col.LambdaFullName.Split('.');
            var tableLambda = string.Join(".", sep.Take(sep.Length - 1).ToArray());
            if (!_lambdaNameAndTable.ContainsKey(tableLambda))
            {
                sep = col.SqlFullName.Split('.');
                var tableSql = string.Join(".", sep.Take(sep.Length - 1).ToArray());
                _lambdaNameAndTable.Add(tableLambda, new TableInfo(tableLambda, tableSql));
            }
        }
    }
}

﻿using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.ConverterServices.Inside
{
    class DbInfo
    {
        Dictionary<string, ColumnInfo> _lambdaNameAndColumn = new Dictionary<string, ColumnInfo>();
        Dictionary<string, TableInfo> _lambdaNameAndTable = new Dictionary<string, TableInfo>();

        internal Dictionary<string, ColumnInfo> GetLambdaNameAndColumn() => _lambdaNameAndColumn.ToDictionary(e=>e.Key, e=>e.Value);
        internal Dictionary<string, TableInfo> GetLambdaNameAndTable() => _lambdaNameAndTable.ToDictionary(e => e.Key, e => e.Value);

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
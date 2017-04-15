using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.ConverterServices.Inside
{
    class DbInfo
    {
        Dictionary<string, ColumnInfo> _lambdaNameAndColumn = new Dictionary<string, ColumnInfo>();
        Dictionary<string, TableInfo> _lambdaNameAndTable = new Dictionary<string, TableInfo>();
        Dictionary<string, string> _lambdaNameSchema = new Dictionary<string, string>();

        internal void Add(ColumnInfo col)
        {
            _lambdaNameAndColumn.Add(col.LambdaFullName, col);

            var sepLambda = col.LambdaFullName.Split('.');
            var tableLambda = string.Join(".", sepLambda.Take(sepLambda.Length - 1).ToArray());
            if (!_lambdaNameAndTable.ContainsKey(tableLambda))
            {
                var sepSql = col.SqlFullName.Split('.');
                var tableSql = string.Join(".", sepSql.Take(sepSql.Length - 1).ToArray());
                _lambdaNameAndTable.Add(tableLambda, new TableInfo(tableLambda, tableSql));

                //has schema.
                if (2 < sepSql.Length && 2 < sepLambda.Length)
                {
                    _lambdaNameSchema[sepLambda[0]] = sepSql[0];
                }
            }
        }

        internal bool TryGetTable(string name, out TableInfo table)
            => _lambdaNameAndTable.TryGetValue(name, out table);

        internal bool TryGetColumn(string name, out ColumnInfo col)
            => _lambdaNameAndColumn.TryGetValue(name, out col);

        internal bool TryGetSchema(string name, out string schema)
            => _lambdaNameSchema.TryGetValue(name, out schema);

        internal DbInfo Clone()
        {
            var clone = new DbInfo();
            clone._lambdaNameAndColumn = _lambdaNameAndColumn.ToDictionary(e => e.Key, e => e.Value);
            clone._lambdaNameAndTable = _lambdaNameAndTable.ToDictionary(e => e.Key, e => e.Value);
            clone._lambdaNameSchema = _lambdaNameSchema.ToDictionary(e => e.Key, e => e.Value);
            return clone;
        }
    }
}

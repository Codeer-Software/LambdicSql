using LambdicSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    public class DbInfo
    {
        Dictionary<string, ColumnInfo> _lambdaNameAndColumn = new Dictionary<string, ColumnInfo>();
        Dictionary<string, TableInfo> _lambdaNameAndTable = new Dictionary<string, TableInfo>();
        public Dictionary<string, ColumnInfo> GetLambdaNameAndColumn() => _lambdaNameAndColumn.ToDictionary(e=>e.Key, e=>e.Value);
        public Dictionary<string, TableInfo> GetLambdaNameAndTable() => _lambdaNameAndTable.ToDictionary(e => e.Key, e => e.Value);

        public SelectClause SelectClause { get; internal set; }//TODO

        internal void Add(ColumnInfo col)
        {
            _lambdaNameAndColumn.Add(col.LambdaFullName, col);

            var sep = col.LambdaFullName.Split('.');
            var tableLambda = string.Join(".", sep.Take(sep.Length - 1).ToArray());
            if (!_lambdaNameAndTable.ContainsKey(tableLambda))
            {
                sep = col.SqlFullName.Split('.');
                var tableSql = string.Join(".", sep.Take(sep.Length - 1).ToArray());
                _lambdaNameAndTable.Add(tableLambda, new TableInfo(tableLambda, tableSql, null));
            }
        }

        internal DbInfo Clone()
        {
            var clone = new DbInfo();
            clone._lambdaNameAndColumn = GetLambdaNameAndColumn();
            clone._lambdaNameAndTable = GetLambdaNameAndTable();
            return clone;
        }

        internal void AddSubQueryTableInfo(Dictionary<string, Expression> lambdaNameAndSubQuery)
        {
            foreach (var e in lambdaNameAndSubQuery)
            {
                TableInfo table;
                if (!_lambdaNameAndTable.TryGetValue(e.Key, out table))
                {
                    throw new NotSupportedException("can use sub query table only at Sql.Using.");
                }
                _lambdaNameAndTable[e.Key] = new TableInfo(table.LambdaFullName, table.SqlFullName, e.Value);
            }
        }
    }
}

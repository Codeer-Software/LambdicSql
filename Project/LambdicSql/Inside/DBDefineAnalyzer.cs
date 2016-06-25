using LambdicSql.QueryInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    static class DBDefineAnalyzer
    {
        internal static Query<T, T> CreateQuery<T>(Expression<Func<T>> define)
           where T : class
        {
            var query = new Query<T, T>();
            query.Create = ExpressionAnalyzer.ToCreateUseDbResult<T>((NewExpression)define.Body);
            query.Db = new DbInfo();
            foreach (var column in FindColumns(typeof(T), new string[0]))
            {
                string schema;
                string table;
                Analyze(column.FullName, out schema, out table);
                query.Db.Add(schema).Add(new[] { schema, table }).Add(column);
            }
            return query;
        }
        
        static void Analyze(IReadOnlyList<string> columnFullName, out string schema, out string table)
        {
            switch (columnFullName.Count)
            {
                case 2:
                    schema = string.Empty;
                    table = columnFullName[0];
                    break;
                case 3:
                    schema = columnFullName[0];
                    table = columnFullName[1];
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        static IEnumerable<ColumnInfo> FindColumns(Type type, IEnumerable<string> names)
        {
            if (type.IsClass && type != typeof(string))
            {
                var list = new List<ColumnInfo>();
                foreach (var p in type.GetProperties().Where(e => e.DeclaringType == type))
                {
                    list.AddRange(FindColumns(p.PropertyType, names.Concat(new[] { p.Name })));
                }
                return list;
            }
            else
            {
                return new[] { new ColumnInfo(type, names.ToArray()) };
            }
        }
    }
}

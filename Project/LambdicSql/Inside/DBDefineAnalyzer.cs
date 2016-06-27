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
            query.Db = new DbInfo();
            foreach (var column in FindColumns(typeof(T), new string[0]))
            {
                query.Db.Add(column);
            }
            var indexInSelect = query.Db.LambdaNameAndColumn.Keys.ToList();
            query.Create = ExpressionToCreateFunc.ToCreateUseDbResult<T>(name => indexInSelect.IndexOf(name), define.Body);
            return query;
        }

        static IEnumerable<ColumnInfo> FindColumns(Type type, IEnumerable<string> names)
        {
            if (SupportedTypeSpec.IsSupported(type))
            {
                //TODO@ if exist difference lambda name and sql name, I'll implement the spec. 
                var name = string.Join(".", names.ToArray());
                return new[] { new ColumnInfo(name, name) };
            }
            else if (type.IsClass)
            {
                var list = new List<ColumnInfo>();
                foreach (var p in type.GetProperties().Where(e => e.DeclaringType == type))
                {
                    list.AddRange(FindColumns(p.PropertyType, names.Concat(new[] { p.Name }).ToArray()));
                }
                return list;
            }
            throw new NotSupportedException();
        }
    }
}

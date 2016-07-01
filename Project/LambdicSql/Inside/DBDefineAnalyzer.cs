using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    static class DBDefineAnalyzer
    {
        internal static IQuery<T, T> CreateQuery<T>(Expression<Func<T>> define)
           where T : class
        {
            var db = new DbInfo();
            foreach (var column in FindColumns(typeof(T), new string[0]))
            {
                db.Add(column);
            }
            var indexInSelect = db.GetLambdaNameAndColumn().Keys.ToList();
            var create = ExpressionToCreateFunc.ToCreateUseDbResult<T>(name => indexInSelect.IndexOf(name), define.Body);
            return new ClauseMakingQuery<T, T, IClause>(db, create, new IClause[0]);
        }

        static IEnumerable<ColumnInfo> FindColumns(Type type, IEnumerable<string> names)
        {
            if (SupportedTypeSpec.IsSupported(type))
            {
                //TODO@ if exist difference lambda name and sql name, I'll implement the spec. 
                var name = string.Join(".", names.ToArray());
                return new[] { new ColumnInfo(type, name, name) };
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

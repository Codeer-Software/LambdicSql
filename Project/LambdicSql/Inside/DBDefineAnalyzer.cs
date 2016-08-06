using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    static class DBDefineAnalyzer
    {
        static Dictionary<Type, DbInfo> _dbInfos = new Dictionary<Type, DbInfo>();
        
        internal static DbInfo GetDbInfo<T>(Expression<Func<T>> define)
        {
            DbInfo db;
            lock (_dbInfos)
            {
                if (_dbInfos.TryGetValue(typeof(T), out db))
                {
                    return db.Clone();
                }
            }

            db = new DbInfo();
            foreach (var column in FindColumns(typeof(T), new string[0]))
            {
                db.Add(column);
            }

            lock (_dbInfos)
            {
                if (!_dbInfos.ContainsKey(typeof(T)))
                {
                    _dbInfos.Add(typeof(T), db);
                }
            }
            return db;
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
                foreach (var p in type.GetProperties())
                {
                    list.AddRange(FindColumns(p.PropertyType, names.Concat(new[] { p.Name }).ToArray()));
                }
                return list;
            }
            throw new NotSupportedException();
        }
    }
}

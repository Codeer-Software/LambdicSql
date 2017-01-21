using System;
using System.Collections.Generic;
using System.Linq;
using LambdicSql.MultiplatformCompatibe;

namespace LambdicSql.ConverterServices.Inside
{
    static class DBDefineAnalyzer
    {
        static Dictionary<Type, DbInfo> _dbInfos = new Dictionary<Type, DbInfo>();
        
        internal static DbInfo GetDbInfo<T>()
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
            foreach (var column in FindColumns(typeof(T), new string[0], new string[0]))
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

        static IEnumerable<ColumnInfo> FindColumns(Type type, IEnumerable<string> lambdicNames, string[] sqlNames)
        {
            //for entity framework.
            if (type.IsGenericTypeEx()) type = type.GetGenericArgumentsEx()[0];
            
            if (SupportedTypeSpec.IsSupported(type))
            {
                var lambdicName = string.Join(".", lambdicNames.ToArray());
                var sqlName = string.Join(".", sqlNames.ToArray());
                return new[] { new ColumnInfo(type, lambdicName, sqlName, sqlNames[sqlNames.Length - 1]) };
            }
            else if (type.IsClassEx())
            {
                var list = new List<ColumnInfo>();
                foreach (var p in type.GetPropertiesEx())
                {
                    //for entity framework.
                    if (p.DeclaringType.FullName == "System.Data.Entity.DbContext") continue;

                    list.AddRange(FindColumns(p.PropertyType,
                        lambdicNames.Concat(new[] { p.Name }).ToArray(),
                        sqlNames.Concat(new[] { ReflectionAdapter.GetSqlName(p) }).ToArray()));
                }
                return list;
            }
            throw new NotSupportedException();
        }

    }
}

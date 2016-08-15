using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
                    return db;
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

        static IEnumerable<ColumnInfo> FindColumns(Type type, IEnumerable<string> lambdicNames, IEnumerable<string> sqlNames)
        {
            //for entity framework.
            if (type.IsGenericType) type = type.GetGenericArguments()[0];

            if (SupportedTypeSpec.IsSupported(type))
            {
                var lambdicName = string.Join(".", lambdicNames.ToArray());
                var sqlName = string.Join(".", sqlNames.ToArray());
                return new[] { new ColumnInfo(type, lambdicName, sqlName) };
            }
            else if (type.IsClass)
            {
                var list = new List<ColumnInfo>();
                foreach (var p in type.GetProperties())
                {
                    //for entity framework.
                    if (p.DeclaringType.FullName == "System.Data.Entity.DbContext") continue;

                    list.AddRange(FindColumns(p.PropertyType, 
                        lambdicNames.Concat(new[] { p.Name }).ToArray(),
                        sqlNames.Concat(new[] { GetSqlName(p) }).ToArray()));
                }
                return list;
            }
            throw new NotSupportedException();
        }

        static string GetSqlName(PropertyInfo p)
        {
            var tableAttr = p.PropertyType.GetCustomAttributes(true).Where(e => e.GetType().FullName == "System.ComponentModel.DataAnnotations.Schema.TableAttribute").FirstOrDefault();
            if (tableAttr != null)
            {
                var name = tableAttr.GetType().GetProperty("Name").GetValue(tableAttr, new object[0]);
                if (name != null) return name.ToString();
            }
            var columnAttr = p.GetCustomAttributes(true).Where(e => e.GetType().FullName == "System.ComponentModel.DataAnnotations.Schema.ColumnAttribute").FirstOrDefault();
            if (columnAttr != null)
            {
                var name = columnAttr.GetType().GetProperty("Name").GetValue(columnAttr, new object[0]);
                if (name != null) return name.ToString();
            }
            return p.Name;
        }
    }
}

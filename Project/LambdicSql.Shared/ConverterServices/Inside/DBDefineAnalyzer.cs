using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LambdicSql.MultiplatformCompatibe;

namespace LambdicSql.ConverterServices.Inside
{
    static class DBDefineAnalyzer
    {
        static Dictionary<Type, DbInfo> _dbInfos = new Dictionary<Type, DbInfo>();

        internal static DbInfo GetDbInfo<T>() where T : class
        {
            DbInfo db;
            lock (_dbInfos)
            {
                if (_dbInfos.TryGetValue(typeof(T), out db))
                {
                    return db.Clone();
                }
            }

            var checkRecursive = new List<Type>();
            db = new DbInfo();
            foreach (var column in FindColumns<T>(typeof(T), new string[0], new string[0], checkRecursive))
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

        static IEnumerable<ColumnInfo> FindColumns<T>(Type type, IEnumerable<string> lambdicNames, string[] sqlNames, List<Type> checkRecursive) where T : class
        {
            //for entity framework.
            if (type.IsGenericTypeEx()) type = type.GetGenericArgumentsEx()[0];

            var defineCustomizer = Db<T>.DefinitionCustomizer;

            if (SupportedTypeSpec.IsSupported(type))
            {
                var lambdicName = string.Join(".", lambdicNames.ToArray());
                var sqlName = string.Join(".", sqlNames.ToArray());
                return new[] { new ColumnInfo(type, lambdicName, sqlName, sqlNames[sqlNames.Length - 1]) };
            }
            else if (type.IsClassEx())
            {
                //check recursive.
                if (checkRecursive.Contains(type)) return new List<ColumnInfo>();
                checkRecursive.Add(type);

                var list = new List<ColumnInfo>();
                foreach (var p in type.GetPropertiesEx())
                {
                    //for entity framework.
                    if (p.DeclaringType.FullName == "System.Data.Entity.DbContext") continue;

                    list.AddRange(FindColumns<T>(p.PropertyType,
                        lambdicNames.Concat(new[] { p.Name }).ToArray(),
                        sqlNames.Concat(new[] { GetSqlName(defineCustomizer, p) }).ToArray(), new List<Type>(checkRecursive)));
                }
                return list;
            }
            throw new NotSupportedException(type.FullName);
        }

        internal static string GetSqlName(DbDefinitionCustomizerDelegate defineCustomizer, PropertyInfo property)
        {
            if (defineCustomizer != null)
            {
                var name = defineCustomizer(property);
                if (!string.IsNullOrEmpty(name)) return name;
            }
            return ReflectionAdapter.GetSqlName(property);
        }
    }
}

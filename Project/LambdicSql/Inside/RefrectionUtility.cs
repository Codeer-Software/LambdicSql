using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.Inside
{
    static class RefrectionUtility
    {
        static Dictionary<string, Func<object, object>> _propertyValue = new Dictionary<string, Func<object, object>>();
        static Dictionary<string, DbParam> _paramTypes = new Dictionary<string, DbParam>();
        static Dictionary<string, bool> _dbGenerated = new Dictionary<string, bool>();

        internal static object GetPropertyValue(this Type type, string name, object obj)
        {
            string cacheName = type.FullName + "." + name;
            lock (_propertyValue)
            {
                Func<object, object> func;
                if (_propertyValue.TryGetValue(cacheName, out func)) return func(obj);

                var methodToString = type.GetProperty(name).GetGetMethod();
                var target = Expression.Parameter(typeof(object), "target");
                func = Expression.Lambda<Func<object, object>>
                    (Expression.Convert(Expression.Call(Expression.Convert(target, type), methodToString, new Expression[0]), typeof(object)), new[] { target }).Compile();

                _propertyValue.Add(cacheName, func);
                return func(obj);
            }
        }

        internal static DbParam GetPropertyParamType(this Type type, string name)
        {
            string cacheName = type.FullName + "." + name;
            lock (_paramTypes)
            {
                DbParam param = null;
                if (_paramTypes.TryGetValue(cacheName, out param)) return param?.Clone();

                //カラムタイプを取得する
                var attrs = type.GetProperty(name).GetCustomAttributes(true);
                var columnAttr = attrs.Where(e => e.GetType().FullName == "System.ComponentModel.DataAnnotations.Schema.ColumnAttribute").FirstOrDefault();
                if (columnAttr != null)
                {
                    var typeName = columnAttr.GetType().GetProperty("TypeName").GetValue(columnAttr, new object[0]);
                    if (typeName != null)
                    {
                        param = DbParam.Parse(typeName.ToString());
                    }
                }
                _paramTypes.Add(cacheName, param);
                return param?.Clone();
            }
        }
    }
}

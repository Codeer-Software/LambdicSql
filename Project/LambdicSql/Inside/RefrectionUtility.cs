using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    static class RefrectionUtility
    {
        static Dictionary<string, Func<object, object>> _propertyValue = new Dictionary<string, Func<object, object>>();

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
    }
}

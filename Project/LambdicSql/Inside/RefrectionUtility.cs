using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using LambdicSql.SqlBase;

namespace LambdicSql.Inside
{
    static class RefrectionUtility
    {
        static Dictionary<string, Func<object, object>> _propertyValue = new Dictionary<string, Func<object, object>>();
        static Dictionary<string, string> _methodGroup = new Dictionary<string, string>();

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

        internal static string GetMethodGroupName(this MethodInfo method)
        {
            string cacheName = method.DeclaringType.FullName + "." + method.Name + "(" +
                string.Join(",", method.GetParameters().Select(e => e.ParameterType.FullName).ToArray()) + ")";
            lock (_methodGroup)
            {
                string groupName;
                if (_methodGroup.TryGetValue(cacheName, out groupName)) return groupName;

                var attr = method.GetCustomAttributes(typeof(MethodGroupAttribute), true).Cast<MethodGroupAttribute>().FirstOrDefault();
                groupName = attr == null ? string.Empty : attr.GroupName;
                _methodGroup[cacheName] = groupName;
                return groupName;
            }
        }
    }
}

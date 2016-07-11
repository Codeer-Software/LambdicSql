using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.Inside
{
    static class SupportedTypeSpec
    {
        static List<Type> _supported = new List<Type>();

        static SupportedTypeSpec()
        {
            _supported.AddRange(typeof(ISqlResult).GetMethods().Where(e=>e.DeclaringType == typeof(ISqlResult)).Select(e=>e.ReturnType));
        }

        internal static bool IsSupported(Type type)
        {
            lock (_supported)
            {
                return _supported.Contains(type);
            }
        }

        internal static string GetFuncName(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return "Get" + type.GetGenericArguments()[0].Name + "Nullable";
            }
            return "Get" + type.Name;
        }
    }
}

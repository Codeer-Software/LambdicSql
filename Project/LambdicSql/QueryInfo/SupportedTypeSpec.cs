using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.QueryInfo
{
    public static class SupportedTypeSpec
    {
        static List<Type> _supported = new List<Type>();

        static SupportedTypeSpec()
        {
            _supported.AddRange(typeof(IDbResult).GetMethods().Where(e=>e.DeclaringType == typeof(IDbResult)).Select(e=>e.ReturnType));
        }

        public static bool IsSupported(Type type)
        {
            lock (_supported)
            {
                return _supported.Contains(type);
            }
        }
    }
}

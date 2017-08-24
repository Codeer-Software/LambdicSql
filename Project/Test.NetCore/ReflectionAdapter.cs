using System;
using System.Reflection;

namespace Test
{
    static class ReflectionAdapter
    {
        internal static bool IsAssignableFromEx(this Type type, Type target)
            => type.GetTypeInfo().IsAssignableFrom(target.GetTypeInfo());
    }
}

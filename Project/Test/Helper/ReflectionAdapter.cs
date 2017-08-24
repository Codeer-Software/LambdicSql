using System;

namespace Test
{
    static class ReflectionAdapter
    {
        internal static bool IsAssignableFromEx(this Type type, Type target) => type.IsAssignableFrom(target);
    }
}

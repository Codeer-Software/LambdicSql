using System;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.MultiplatformCompatibe
{
    //TODO
    static class ReflectionAdapter
    {
        internal static T GetAttribute<T>(this MemberInfo member) where T : class => null;
        internal static T GetAttribute<T>(this Type type) where T : class => null;
        internal static bool IsAssignableFromEx(this Type type, Type target) => false;
        internal static PropertyInfo[] GetPropertiesEx(this Type type) => null;

        internal static string GetSqlName(PropertyInfo p) => p.Name;

        public static Type[] GetGenericArgumentsEx(this Type type) => null;

        public static bool IsClassEx(this Type type) => false;
        internal static bool IsGenericTypeEx(this Type type) => false;
        internal static MemberExpression StaticPropertyOrField(Type type, string propertyOrFieldName) => null;
    }
}

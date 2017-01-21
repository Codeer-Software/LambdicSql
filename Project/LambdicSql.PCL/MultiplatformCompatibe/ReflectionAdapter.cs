using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace LambdicSql.MultiplatformCompatibe
{
    //TODO
    static class ReflectionAdapter
    {
        internal static T GetAttribute<T>(this MemberInfo member) where T : Attribute
            => member.GetCustomAttribute<T>();

        internal static T GetAttribute<T>(this Type type) where T : Attribute
            => type.GetTypeInfo().GetCustomAttribute<T>();

        internal static bool IsAssignableFromEx(this Type type, Type target) 
            => type.GetTypeInfo().IsAssignableFrom(target.GetTypeInfo());

        internal static PropertyInfo[] GetPropertiesEx(this Type type)
            => type.GetTypeInfo().DeclaredProperties.ToArray();

        public static Type[] GetGenericArgumentsEx(this Type type)
            => type.GetTypeInfo().GetGenericParameterConstraints();

        public static bool IsClassEx(this Type type)
            => type.GetTypeInfo().IsClass;

        internal static bool IsGenericTypeEx(this Type type)
            => type.GetTypeInfo().IsGenericType;

        internal static string GetSqlName(PropertyInfo p) => p.Name;

        internal static MemberExpression StaticPropertyOrField(Type type, string propertyOrFieldName)
        {
            var property = type.GetTypeInfo().GetDeclaredProperty(propertyOrFieldName);
            if (property != null) return Expression.Property(null, property);

            var field = type.GetTypeInfo().GetDeclaredField(propertyOrFieldName);
            if (field != null) return Expression.Field(null, field);

            throw new NotSupportedException();
        }
    }
}

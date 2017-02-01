using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace LambdicSql.MultiplatformCompatibe
{
    static class ReflectionAdapter
    {
        internal static object CreateInstance(Type type, bool nonPublic)
            => Activator.CreateInstance(type);

        internal static T GetAttribute<T>(this MemberInfo info) where T : Attribute
            => info.GetCustomAttribute<T>();

        internal static T GetAttribute<T>(this ParameterInfo info) where T : Attribute
            => info.GetCustomAttribute<T>();

        internal static T GetAttribute<T>(this FieldInfo info) where T : Attribute
            => info.GetCustomAttribute<T>();

        internal static T GetAttribute<T>(this Type type) where T : Attribute
            => type.GetTypeInfo().GetCustomAttribute<T>();
        
        internal static bool IsClassAndAssignableFromEx(this Type type, Type target)
            => target.IsClassEx() && type.IsAssignableFromEx(target);

        internal static bool IsAssignableFromEx(this Type type, Type target)
            => type.GetTypeInfo().IsAssignableFrom(target.GetTypeInfo());

        internal static FieldInfo[] GetFieldsEx(this Type type) => type.GetTypeInfo().DeclaredFields.ToArray();

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

using System;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.MultiplatformCompatibe
{
    static class ReflectionAdapter
    {
        internal static object CreateInstance(Type type, bool nonPublic)
            => Activator.CreateInstance(type, nonPublic);

        internal static bool IsClass(this Type type) => type.IsClass;

        internal static T GetAttribute<T>(this MemberInfo member) where T : Attribute
        {
            var attrs = member.GetCustomAttributes(typeof(T), true);
            return attrs.Length == 1 ?
                attrs[0] as T :
                null;
        }

        internal static T GetAttribute<T>(this Type type) where T : Attribute
        {
            var attrs = type.GetCustomAttributes(typeof(T), true);
            return attrs.Length == 1 ?
                attrs[0] as T :
                null;
        }

        internal static T GetAttribute<T>(this ParameterInfo info) where T : Attribute
        {
            var attrs = info.GetCustomAttributes(typeof(T), true);
            return attrs.Length == 1 ?
                attrs[0] as T :
                null;
        }

        internal static T GetAttribute<T>(this FieldInfo info) where T : Attribute
        {
            var attrs = info.GetCustomAttributes(typeof(T), true);
            return attrs.Length == 1 ?
                attrs[0] as T :
                null;
        }

        internal static bool IsClassAndAssignableFromEx(this Type type, Type target)
            => target.IsClassEx() && type.IsAssignableFromEx(target);

        internal static bool IsAssignableFromEx(this Type type, Type target) => type.IsAssignableFrom(target);

        internal static FieldInfo[] GetFieldsEx(this Type type) => type.GetFields();

        internal static PropertyInfo[] GetPropertiesEx(this Type type) => type.GetProperties();

        internal static Type[] GetGenericArgumentsEx(this Type type) => type.GetGenericArguments();

        internal static bool IsClassEx(this Type type) => type.IsClass;

        internal static bool IsGenericTypeEx(this Type type) => type.IsGenericType;

        internal static DbDefinitionCustomizerDelegate GetDefineCustomizer(Type t)
        {
            if (!t.IsClass) return null;
            var dbType = typeof(Db<>).MakeGenericType(new[] { t });
            return (DbDefinitionCustomizerDelegate)dbType.GetProperty(nameof(Db<Non>.DefinitionCustomizer)).GetValue(null, new object[0]);
        }

        internal static string GetSqlName(PropertyInfo property)
        {
            var type = property.PropertyType;

            //for entity framework.
            if (type.IsGenericType) type = type.GetGenericArguments()[0];

            var tableAttr = type.GetCustomAttributes(true).Where(e => e.GetType().IsAssignableFromByTypeFullName("System.ComponentModel.DataAnnotations.Schema.TableAttribute")).FirstOrDefault();
            if (tableAttr != null)
            {
                var name = tableAttr.GetType().GetProperty("Name").GetValue(tableAttr, new object[0]);
                if (name != null) return name.ToString();
            }
            var columnAttr = property.GetCustomAttributes(true).Where(e => e.GetType().IsAssignableFromByTypeFullName("System.ComponentModel.DataAnnotations.Schema.ColumnAttribute")).FirstOrDefault();
            if (columnAttr != null)
            {
                var name = columnAttr.GetType().GetProperty("Name").GetValue(columnAttr, new object[0]);
                if (name != null) return name.ToString();
            }
            return property.Name;
        }

        internal static bool IsAssignableFromByTypeFullName(this Type type, string typeFullName)
        {
            while (type != null)
            {
                if (type.FullName == typeFullName)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }

        internal static MemberExpression StaticPropertyOrField(Type type, string propertyOrFieldName)
        {
            var flgs = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            var property = type.GetProperty(propertyOrFieldName, flgs);
            if (property != null) return Expression.Property(null, property);

            var field = type.GetField(propertyOrFieldName, flgs);
            if (field != null) return Expression.Field(null, field);

            throw new NotSupportedException();
        }
    }
}

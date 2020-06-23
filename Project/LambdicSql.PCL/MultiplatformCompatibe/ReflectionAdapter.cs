using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

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

        internal static FieldInfo[] GetFieldsEx(this Type type)
        {
            var list = new List<FieldInfo>();
            while (type != null)
            {
                var info = type.GetTypeInfo();
                list.AddRange(info.DeclaredFields);
                type = info.BaseType;
            }
            return list.ToArray();
        }

        internal static PropertyInfo[] GetPropertiesEx(this Type type)
        {
            var list = new List<PropertyInfo>();
            while (type != null)
            {
                var info = type.GetTypeInfo();
                list.AddRange(info.DeclaredProperties);
                type = info.BaseType;
            }
            return list.ToArray();
        }

        public static Type[] GetGenericArgumentsEx(this Type type)
            => type.GenericTypeArguments;

        public static bool IsClassEx(this Type type)
            => type.GetTypeInfo().IsClass;

        internal static bool IsGenericTypeEx(this Type type)
            => type.GetTypeInfo().IsGenericType;

        internal static DbDefinitionCustomizerDelegate GetDefineCustomizer(Type t)
        {
            if (!t.GetTypeInfo().IsClass) return null;
            var dbType = typeof(Db<>).MakeGenericType(new[] { t });
            var info = dbType.GetTypeInfo();
            return (DbDefinitionCustomizerDelegate)info.GetDeclaredProperty(nameof(Db<Non>.DefinitionCustomizer)).GetValue(null, new object[0]);
        }

        internal static string GetSqlName(PropertyInfo property)
        {
            var type = property.PropertyType;

            //for entity framework.
            if (type.IsGenericTypeEx()) type = type.GetGenericArgumentsEx()[0];

            var tableAttr = type.GetTypeInfo().GetCustomAttributes(true).Where(e => e.GetType().IsAssignableFromByTypeFullName("System.ComponentModel.DataAnnotations.Schema.TableAttribute")).FirstOrDefault();
            if (tableAttr != null)
            {
                var name = tableAttr.GetType().GetTypeInfo().GetDeclaredProperty("Name").GetValue(tableAttr, new object[0]);
                if (name != null) return name.ToString();
            }
            var columnAttr = property.GetCustomAttributes(true).Where(e => e.GetType().IsAssignableFromByTypeFullName("System.ComponentModel.DataAnnotations.Schema.ColumnAttribute")).FirstOrDefault();
            if (columnAttr != null)
            {
                var name = columnAttr.GetType().GetTypeInfo().GetDeclaredProperty("Name").GetValue(columnAttr, new object[0]);
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
                type = type.GetTypeInfo().BaseType;
            }
            return false;
        }

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

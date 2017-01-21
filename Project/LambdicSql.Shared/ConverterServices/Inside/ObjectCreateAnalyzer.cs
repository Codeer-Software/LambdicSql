using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System;
using System.Collections.Generic;
using LambdicSql.MultiplatformCompatibe;

namespace LambdicSql.ConverterServices.Inside
{
    static class ObjectCreateAnalyzer
    {
        static Dictionary<Type, ObjectCreateInfo> _selectedTypeInfo = new Dictionary<Type, ObjectCreateInfo>();

        internal static ObjectCreateInfo MakeSelectInfo(Type type)
        {
            lock (_selectedTypeInfo)
            {
                ObjectCreateInfo info;
                if (_selectedTypeInfo.TryGetValue(type, out info)) return info;

                info = new ObjectCreateInfo(type.GetPropertiesEx().Select(e=> new ObjectCreateMemberInfo(e.Name, null)), null);
                _selectedTypeInfo[type] = info;
                return info;
            }
        }

        internal static ObjectCreateInfo MakeSelectInfo(Expression exp)
        {
            var select = new List<ObjectCreateMemberInfo>();
            var newExp = exp as NewExpression;
            if (newExp != null)
            {
                for (int i = 0; i < newExp.Arguments.Count; i++)
                {
                    var propInfo = newExp.Members[i] as PropertyInfo;
                    string name = null;
                    if (propInfo != null)
                    {
                        name = propInfo.Name;
                    }
                    else
                    {
                        //.net3.5
                        var method = newExp.Members[i] as MethodInfo;
                        name = method.GetPropertyName();
                    }
                    select.Add(new ObjectCreateMemberInfo(name, newExp.Arguments[i]));
                }
                return new ObjectCreateInfo(select, exp);
            }

            var initExp = exp as MemberInitExpression;
            if (initExp != null)
            {
                var elements = new ObjectCreateMemberInfo[initExp.Bindings.Count];
                for (int i = 0; i < elements.Length; i++)
                {
                    var e = initExp.Bindings[i] as MemberAssignment;
                    elements[i] = new ObjectCreateMemberInfo(e.Member.Name, e.Expression);
                }
                return new ObjectCreateInfo(elements, exp);
            }

            var member = exp as MemberExpression;
            if (member != null)
            {
                if (SupportedTypeSpec.IsSupported(exp.Type))
                {
                    return new ObjectCreateInfo(new[] { new ObjectCreateMemberInfo(string.Empty, exp) }, exp);
                }
                Type type = null;
                var prop = member.Member as PropertyInfo;
                if (prop != null) type = prop.PropertyType;
                else type = ((FieldInfo)member.Member).FieldType;
                return MakeSelectInfo(type);
            }

            return new ObjectCreateInfo(new[] { new ObjectCreateMemberInfo(string.Empty, exp )}, exp);
        }

        static string GetPropertyName(this MethodInfo method)
            => (method.Name.IndexOf("get_") == 0) ?
                method.Name.Substring(4) : 
                method.Name;
    }
}

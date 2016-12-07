using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System;
using LambdicSql;
using System.Collections.Generic;
using LambdicSql.SqlBase;

namespace LambdicSql.Inside
{
    internal static class ObjectCreateAnalyzer
    {
        internal static ObjectCreateInfo MakeSelectInfo(Type type)
        {
            var select = new List<ObjectCreateMemberInfo>();
            foreach (var p in type.GetProperties())
            {
                select.Add(new ObjectCreateMemberInfo(p.Name, null));
            }
            return new ObjectCreateInfo(select, null);
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
                foreach (var b in initExp.Bindings.Cast<MemberAssignment>())
                {
                    select.Add(new ObjectCreateMemberInfo(b.Member.Name, b.Expression));
                }
                return new ObjectCreateInfo(select, exp);
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

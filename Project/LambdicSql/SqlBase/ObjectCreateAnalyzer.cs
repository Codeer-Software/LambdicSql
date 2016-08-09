using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System;
using LambdicSql;
using System.Collections.Generic;

namespace LambdicSql.SqlBase
{
    public static class ObjectCreateAnalyzer
    {
        public static ObjectCreateInfo MakeSelectInfo(Expression exp)
        {
            var select = new List<ObjectCreateMemberElement>();
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
                    select.Add(new ObjectCreateMemberElement(name, newExp.Arguments[i]));
                }
                return new ObjectCreateInfo(select, exp);
            }
            var initExp = exp as MemberInitExpression;
            if (initExp != null)
            {
                foreach (var b in initExp.Bindings.Cast<MemberAssignment>())
                {
                    select.Add(new ObjectCreateMemberElement(b.Member.Name, b.Expression));
                }
                return new ObjectCreateInfo(select, exp);
            }
            var member = exp as MemberExpression;
            if (member != null)
            {
                var type = ((PropertyInfo)member.Member).PropertyType;
                foreach (var p in type.GetProperties())
                {
                    select.Add(new ObjectCreateMemberElement(p.Name, null));
                }
                return new ObjectCreateInfo(select, exp);
            }
            throw new NotSupportedException();
        }

        static string GetPropertyName(this MethodInfo method)
            => (method.Name.IndexOf("get_") == 0) ?
                method.Name.Substring(4) : method.Name;
    }
}

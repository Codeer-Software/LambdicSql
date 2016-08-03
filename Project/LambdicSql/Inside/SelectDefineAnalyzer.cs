using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System;
using LambdicSql.Words;

namespace LambdicSql.Inside
{
    static class SelectDefineAnalyzer
    {
        internal static SelectClause MakeSelectInfo(Expression exp)
        {
            var select = new SelectClause();
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
                    select.Add(new SelectElement(name, newExp.Arguments[i]));
                }
                return select;
            }
            var initExp = exp as MemberInitExpression;
            if (initExp != null)
            {
                foreach (var b in initExp.Bindings.Cast<MemberAssignment>())
                {
                    select.Add(new SelectElement(b.Member.Name, b.Expression));
                }
                return select;
            }
            var member = exp as MemberExpression;
            if (member != null)
            {
                var type = ((PropertyInfo)member.Member).PropertyType;
                foreach (var p in type.GetProperties().Where(e => e.DeclaringType == type))
                {
                    select.Add(new SelectElement(p.Name, null));
                }
                return select;
            }
            throw new NotSupportedException();
        }
    }
}

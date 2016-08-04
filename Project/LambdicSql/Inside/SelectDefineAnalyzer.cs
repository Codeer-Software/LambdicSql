using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System;
using LambdicSql;
using System.Collections.Generic;

namespace LambdicSql.Inside
{
    static class SelectDefineAnalyzer
    {
        internal static SelectClauseInfo MakeSelectInfo(Expression exp)
        {
            var select = new List<SelectElement>();
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
                return new SelectClauseInfo(select, exp);
            }
            var initExp = exp as MemberInitExpression;
            if (initExp != null)
            {
                foreach (var b in initExp.Bindings.Cast<MemberAssignment>())
                {
                    select.Add(new SelectElement(b.Member.Name, b.Expression));
                }
                return new SelectClauseInfo(select, exp);
            }
            var member = exp as MemberExpression;
            if (member != null)
            {
                var type = ((PropertyInfo)member.Member).PropertyType;
                foreach (var p in type.GetProperties())
                {
                    select.Add(new SelectElement(p.Name, null));
                }
                return new SelectClauseInfo(select, exp);
            }
            throw new NotSupportedException();
        }
    }
}

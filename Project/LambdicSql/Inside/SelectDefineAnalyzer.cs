using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

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
                        name = method.Name;
                    }

                    var argMember = newExp.Arguments[i] as MemberExpression;
                    select.Add(new SelectElement(name, newExp.Arguments[i]));
                }
            }
            else
            {
                var initExp = (MemberInitExpression)exp;
                foreach (var b in initExp.Bindings.Cast<MemberAssignment>())
                {
                    select.Add(new SelectElement(b.Member.Name, b.Expression));
                }
            }
            return select;
        }
    }
}

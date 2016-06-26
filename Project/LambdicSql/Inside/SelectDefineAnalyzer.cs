using LambdicSql.QueryInfo;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace LambdicSql.Inside
{
    static class SelectDefineAnalyzer
    {
        internal static SelectInfo MakeSelectInfo(Expression exp)
        {
            var select = new SelectInfo();
            var newExp = exp as NewExpression;
            if (newExp != null)
            {
                for (int i = 0; i < newExp.Arguments.Count; i++)
                {
                    var propInfo = newExp.Members[i] as PropertyInfo;
                    var argMember = newExp.Arguments[i] as MemberExpression;
                    select.Add(new SelectElementInfo(propInfo.Name, newExp.Arguments[i]));
                }
            }
            else
            {
                var initExp = (MemberInitExpression)exp;
                foreach (var b in initExp.Bindings.Cast<MemberAssignment>())
                {
                    select.Add(new SelectElementInfo(b.Member.Name, b.Expression));
                }
                
                //+		b.GetType()	{Name = "MemberAssignment" FullName = "System.Linq.Expressions.MemberAssignment"}	System.Type {System.RuntimeType}

            }




            return select;
        }
    }
}

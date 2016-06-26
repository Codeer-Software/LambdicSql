using LambdicSql.QueryInfo;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    static class SelectDefineAnalyzer
    {
        internal static SelectInfo MakeSelectInfo(NewExpression exp)
        {
            var select = new SelectInfo();
            for (int i = 0; i < exp.Arguments.Count; i++)
            {
                var propInfo = exp.Members[i] as PropertyInfo;
                var argMember = exp.Arguments[i] as MemberExpression;
                select.Add(new SelectElementInfo(propInfo.Name, exp.Arguments[i]));
            }
            return select;
        }
    }
}

using System;
using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    public static class SqlSyntaxUtility
    {
        //TODO refactoring.
        public static Func<int, int> SqlSyntaxMethodArgumentAdjuster(this MethodCallExpression exp)
        {
            var ps = exp.Method.GetParameters();
            if (0 < ps.Length && typeof(IMethodChain).IsAssignableFrom(ps[0].ParameterType)) return i => i + 1;
            else return i => i;
        }
    }
}

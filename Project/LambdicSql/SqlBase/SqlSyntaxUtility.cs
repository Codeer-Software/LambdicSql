using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    public static class SqlSyntaxUtility
    {
        public static int AdjustSqlSyntaxMethodArgumentIndex(this MethodCallExpression exp, int index)
        {
            var ps = exp.Method.GetParameters();
            if (0 < ps.Length && typeof(IMethodChain).IsAssignableFrom(ps[0].ParameterType)) return index + 1;
            else return index;
        }
    }
}

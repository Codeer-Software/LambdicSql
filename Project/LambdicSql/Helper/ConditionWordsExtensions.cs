using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class ConditionWordsExtensions
    {
        public static bool Condition(this ISqlUtility words, bool enable, bool condition) => default(bool);

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            object obj;
            ExpressionToObject.GetExpressionObject(method.Arguments[1], out obj);
            return (bool)obj ? converter.ToString(method.Arguments[2]) : string.Empty;
        }
    }
}

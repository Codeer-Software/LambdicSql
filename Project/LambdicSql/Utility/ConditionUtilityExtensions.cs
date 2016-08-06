using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class ConditionUtilityExtensions
    {
        public static bool Condition(this ISqlUtility words, bool enable, bool condition) => default(bool);

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            object obj;
            ExpressionToObject.GetExpressionObject(method.Arguments[1], out obj);
            return (bool)obj ? converter.ToString(method.Arguments[2]) : string.Empty;
        }
    }
}

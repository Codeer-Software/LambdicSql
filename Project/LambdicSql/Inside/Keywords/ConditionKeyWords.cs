using LambdicSql.SqlBase;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class ConditionKeyWords
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = method.Arguments.Select(e => converter.ToString(e)).ToArray();
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.Like): return args[0] + " LIKE " + args[1];
                case nameof(LambdicSql.Keywords.Between): return args[0] + " BETWEEN " + args[1] + " AND " + args[2];
                case nameof(LambdicSql.Keywords.In): return args[0] + " IN(" + string.Join(", ", args.Skip(1).ToArray()) + ")";
            }
            return null;
        }
    }
}

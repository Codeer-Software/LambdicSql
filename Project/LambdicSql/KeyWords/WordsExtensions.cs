using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class WordsExtensions
    {
        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = method.Arguments.Select(e => converter.ToString(e)).ToArray();
            switch (method.Method.Name)
            {
                case nameof(Sql.Like): return args[0] + " LIKE " + args[1];
                case nameof(Sql.Between): return args[0] + " BETWEEN " + args[1] + " AND " + args[2];
                case nameof(Sql.In): return args[0] + " IN(" + string.Join(", ", args.Skip(1).ToArray()) + ")";
            }
            return null;
        }
    }
}

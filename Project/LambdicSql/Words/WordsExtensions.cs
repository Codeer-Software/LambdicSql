using LambdicSql.QueryBase;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class WordsExtensions
    {
        public static bool Like(this ISqlSyntax words, string target, string serachText) => false;
        public static bool Between<TTarget>(this ISqlSyntax words, TTarget target, TTarget min, TTarget max) => false;
        public static bool In<TTarget>(this ISqlSyntax words, TTarget target, params TTarget[] inArguments) => false;

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();
            switch (method.Method.Name)
            {
                case nameof(Like): return args[0] + " LIKE " + args[1];
                case nameof(Between): return args[0] + " BETWEEN " + args[1] + " AND " + args[2];
                case nameof(In): return args[0] + " IN(" + string.Join(", ", args.Skip(1).ToArray()) + ")";
            }
            return null;
        }
    }
}

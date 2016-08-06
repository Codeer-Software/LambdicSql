using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class WordsExtensions
    {
        public static bool Like(this ISqlKeyWord words, string target, string serachText) => InvalitContext.Throw<bool>(nameof(Like));
        public static bool Between<TTarget>(this ISqlKeyWord words, TTarget target, TTarget min, TTarget max) => InvalitContext.Throw<bool>(nameof(Between));
        public static bool In<TTarget>(this ISqlKeyWord words, TTarget target, params TTarget[] inArguments) => InvalitContext.Throw<bool>(nameof(In));

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
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

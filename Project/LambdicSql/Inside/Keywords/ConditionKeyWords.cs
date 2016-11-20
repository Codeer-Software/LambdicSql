using LambdicSql.SqlBase;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace LambdicSql.Inside.Keywords
{
    static class ConditionKeyWords
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = method.Arguments.Select(e => AdjustSubQuery(e, converter.ToString(e))).ToArray();
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.Like): return args[0] + " LIKE " + args[1];
                case nameof(LambdicSql.Keywords.Between): return args[0] + " BETWEEN " + args[1] + " AND " + args[2];
                case nameof(LambdicSql.Keywords.In): return args[0] + " IN(" + string.Join(", ", args.Skip(1).ToArray()) + ")";
                case nameof(LambdicSql.Keywords.Exists): return "EXISTS" + args[0];
            }
            return null;
        }
        
        //TODO refactoring.
        static string AdjustSubQuery(Expression e, string v)
        {
            if (typeof(IQuery).IsAssignableFrom(e.Type))
            {
                return SqlStringConverter.AdjustSubQueryString(v);
            }
            return v;
        }
    }
}

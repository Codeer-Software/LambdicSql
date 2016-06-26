using LambdicSql.QueryInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    static class ExpressionToCreateFunc
    {
        internal static Func<IDbResult, T> ToCreateUseDbResult<T>(IReadOnlyDictionary<string, ColumnInfo> lambdaNameAndColumn, NewExpression exp)
        {
            var param = Expression.Parameter(typeof(IDbResult), "dbResult");
            var arguments = new[] { param };
            return Expression.Lambda<Func<IDbResult, T>>(New(lambdaNameAndColumn, new string[0], exp, param), arguments).Compile();
        }

        static NewExpression New(IReadOnlyDictionary<string, ColumnInfo> lambdaNameAndColumn, string[] names, NewExpression exp, ParameterExpression param)
        {
            return Expression.New(exp.Constructor, ConvertArguments(lambdaNameAndColumn, names, exp.Arguments.ToArray(), exp.Members.ToArray(), param), exp.Members);
        }

        static IEnumerable<Expression> ConvertArguments(IReadOnlyDictionary<string, ColumnInfo> lambdaNameAndColumn, string[] names, Expression[] args, MemberInfo[] members, ParameterExpression param)
        {
            var newArgs = new List<Expression>();
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                var member = members[i] as PropertyInfo;

                var currentNames = names.Concat(new[] { member.Name });

                var newExp = arg as NewExpression;
                if (newExp == null)
                {
                    var name = string.Join(".", currentNames);
                    var sqlName = lambdaNameAndColumn == null ? name : lambdaNameAndColumn[name].SqlFullName;

                    sqlName = sqlName.Replace(".", "@");//TODO@ special spec.

                    newArgs.Add(Expression.Call(param, typeof(IDbResult).GetMethod("Get" + member.PropertyType.Name), Expression.Constant(sqlName)));
                }
                else
                {
                    newArgs.Add(New(lambdaNameAndColumn, currentNames.ToArray(), newExp, param));
                }
            }
            return newArgs;
        }
    }
}

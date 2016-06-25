using LambdicSql.QueryInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    static class ExpressionAnalyzer
    {
        internal static string GetElementName<TDB, T>(this Expression<Func<TDB, T>> exp) where TDB : class
        {
            return GetElementName(exp.Body as MemberExpression);
        }

        internal static string GetElementName(this MemberExpression exp)
        {
            //TODO I'll make best code.
            return string.Join(".", exp.ToString().Split('.').Skip(1));
        }

        internal static Func<IDbResult, T> ToCreateUseDbResult<T>(NewExpression exp)
        {
            var param = Expression.Parameter(typeof(IDbResult), "dbResult");
            var arguments = new[] { param };
            return Expression.Lambda<Func<IDbResult, T>>(New(new string[0], exp, param), arguments).Compile();
        }

        static NewExpression New(string[] names, NewExpression exp, ParameterExpression param)
        {
            return Expression.New(exp.Constructor, ConvertArguments(names, exp.Arguments.ToArray(), exp.Members.ToArray(), param), exp.Members);
        }

        static IEnumerable<Expression> ConvertArguments(string[] names, Expression[] args, MemberInfo[] members, ParameterExpression param)
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
                    newArgs.Add(Expression.Call(param, typeof(IDbResult).GetMethod("Get" + member.PropertyType.Name), Expression.Constant(string.Join(".", currentNames))));
                }
                else
                {
                    newArgs.Add(New(currentNames.ToArray(), newExp, param));
                }
            }
            return newArgs;
        }
    }
}

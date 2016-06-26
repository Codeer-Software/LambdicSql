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
        internal static Func<IDbResult, T> ToCreateUseDbResult<T>(IReadOnlyDictionary<string, ColumnInfo> lambdaNameAndColumn, Expression exp)
        {
            var newExp = exp as NewExpression;
            if (newExp == null)
            {
                newExp = ((MemberInitExpression)exp).NewExpression;
            }
            var param = Expression.Parameter(typeof(IDbResult), "dbResult");
            var arguments = new[] { param };
            return Expression.Lambda<Func<IDbResult, T>>(New(lambdaNameAndColumn, new string[0], newExp, param), arguments).Compile();
        }

        static Expression New(IReadOnlyDictionary<string, ColumnInfo> lambdaNameAndColumn, string[] names, NewExpression exp, ParameterExpression param)
        {
            if (exp.Members == null)
            {
                var binding = new List<MemberBinding>();
                foreach (var p in exp.Type.GetProperties().Where(e => e.DeclaringType == exp.Type))
                {
                    var currentNames = names.Concat(new[] { p.Name }).ToArray();
                    if (SupportedTypeSpec.IsSupported(p.PropertyType))
                    {
                        var name = string.Join(".", currentNames);
                        var sqlName = lambdaNameAndColumn == null ? name : lambdaNameAndColumn[name].SqlFullName;
                        sqlName = sqlName.Replace(".", "@");//TODO@ special spec.
                        var propertyNew = Expression.Call(param, typeof(IDbResult).GetMethod("Get" + p.PropertyType.Name), Expression.Constant(sqlName));
                        binding.Add(Expression.Bind(p, propertyNew));
                    }
                    else
                    {
                        binding.Add(Expression.Bind(p, New(lambdaNameAndColumn, currentNames, Expression.New(p.PropertyType.GetConstructor(new Type[0])), param)));
                    }

                }
                return Expression.MemberInit(Expression.New(exp.Constructor), binding.ToArray());
            }
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

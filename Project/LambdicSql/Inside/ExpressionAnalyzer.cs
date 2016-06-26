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
            //TODO @I'll make best code.
            return string.Join(".", exp.ToString().Split('.').Skip(1));
        }

        internal static Func<IDbResult, T> ToCreateUseDbResult<T>(IReadOnlyDictionary<string, ColumnInfo> lambdaNameAndColumn, NewExpression exp)
        {
            var param = Expression.Parameter(typeof(IDbResult), "dbResult");
            var arguments = new[] { param };
            return Expression.Lambda<Func<IDbResult, T>>(New(lambdaNameAndColumn, new string[0], exp, param), arguments).Compile();
        }

        internal static IEnumerable<object> GetArguments(IReadOnlyDictionary<string, ColumnInfo> dbColumns, MethodCallExpression argMethod)
        {
            var arguments = new List<object>();
            foreach (var arg in argMethod.Arguments.Skip(1))
            {
                var member = arg as MemberExpression;
                if (member != null)
                {
                    var name = member.GetElementName();
                    ColumnInfo col;
                    if (dbColumns.TryGetValue(name, out col))
                    {
                        arguments.Add(col);
                    }
                    else
                    {
                        dynamic func = Expression.Lambda(member).Compile();
                        arguments.Add(func());
                    }
                    continue;
                }
                var constant = arg as ConstantExpression;
                if (constant != null)
                {
                    dynamic func = Expression.Lambda(constant).Compile();
                    arguments.Add(func().ToString());
                    continue;
                }
                throw new NotSupportedException();
            }
            return arguments;
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

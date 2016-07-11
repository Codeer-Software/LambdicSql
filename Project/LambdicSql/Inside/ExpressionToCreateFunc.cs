using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    static class ExpressionToCreateFunc
    {
        internal static Func<ISqlResult, T> ToCreateUseDbResult<T>(Func<string, int> getIndexInSelect, Expression exp)
        {
            var newExp = exp as NewExpression;
            if (newExp == null)
            {
                newExp = ((MemberInitExpression)exp).NewExpression;
            }
            var param = Expression.Parameter(typeof(ISqlResult), "dbResult");
            var arguments = new[] { param };
            try
            {
                return Expression.Lambda<Func<ISqlResult, T>>(New(getIndexInSelect, new string[0], newExp, param), arguments).Compile();
            }
            catch (CanNotCreateException)
            {
                return null;
            }
        }

        static Expression New(Func<string, int> getIndexInSelect, string[] names, NewExpression exp, ParameterExpression param)
        {
            if (exp.Members == null)
            {
                var binding = new List<MemberBinding>();
                foreach (var p in exp.Type.GetProperties().Where(e => e.DeclaringType == exp.Type))
                {
                    var currentNames = names.Concat(new[] { p.Name }).ToArray();
                    if (SupportedTypeSpec.IsSupported(p.PropertyType))
                    {
                        var funcName = SupportedTypeSpec.GetFuncName(p.PropertyType);
                        var name = string.Join(".", currentNames);
                        binding.Add(Expression.Bind(p,
                            Expression.Call(param, typeof(ISqlResult).GetMethod(funcName), Expression.Constant(getIndexInSelect(name)))));
                    }
                    else
                    {
                        var constructor = p.PropertyType.GetConstructor(new Type[0]);
                        if (constructor == null)
                        {
                            throw new CanNotCreateException();
                        }
                        binding.Add(Expression.Bind(p, New(getIndexInSelect, currentNames, Expression.New(constructor), param)));
                    }

                }
                return Expression.MemberInit(Expression.New(exp.Constructor), binding.ToArray());
            }
            return Expression.New(exp.Constructor, ConvertArguments(getIndexInSelect, names, exp.Arguments.ToArray(), exp.Members.ToArray(), param), exp.Members);
        }

        static IEnumerable<Expression> ConvertArguments(Func<string, int> getIndexInSelect, string[] names, Expression[] args, MemberInfo[] members, ParameterExpression param)
        {
            var newArgs = new List<Expression>();
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                var propInfo = members[i] as PropertyInfo;
                string[] currentNames = null;
                Type paramType = null;
                if (propInfo != null)
                {
                    currentNames = names.Concat(new[] { propInfo.Name }).ToArray();
                    paramType = propInfo.PropertyType;
                }
                else
                {
                    //.net3.5
                    var method = members[i] as MethodInfo;
                    paramType = method.ReturnType;
                    currentNames = names.Concat(new[] { method.Name }).ToArray();
                }
                var newExp = arg as NewExpression;
                if (newExp == null)
                {
                    if (SupportedTypeSpec.IsSupported(paramType))
                    {
                        var name = string.Join(".", currentNames);
                        newArgs.Add(Expression.Call(param, typeof(ISqlResult).GetMethod("Get" + paramType.Name), 
                            Expression.Constant(getIndexInSelect(name))));
                    }
                    else
                    {
                        var constructor = paramType.GetConstructor(new Type[0]);
                        if (constructor == null)
                        {
                            throw new CanNotCreateException();
                        }
                        newArgs.Add(New(getIndexInSelect, currentNames.ToArray(), Expression.New(paramType.GetConstructor(new Type[0])), param));
                    }
                }
                else
                {
                    newArgs.Add(New(getIndexInSelect, currentNames.ToArray(), newExp, param));
                }
            }
            return newArgs;
        }
    }
}

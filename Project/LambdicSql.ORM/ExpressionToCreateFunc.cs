using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.ORM
{
    static class ExpressionToCreateFunc
    {
        static Dictionary<string, object> _createMap = new Dictionary<string, object>();

        internal static Func<ISqlResult, T> ToCreateUseDbResult<T>(List<string> getIndexInSelect, Expression exp)
        {
            var name = typeof(T).FullName + "@" + string.Join("@", getIndexInSelect.ToArray());
            lock (_createMap)
            {
                object obj;
                if (_createMap.TryGetValue(name, out obj))
                {
                    return (Func<ISqlResult, T>)obj;
                }
            }
            var func = ToCreateUseDbResultCore<T>(getIndexInSelect, exp);
            lock (_createMap)
            {
                if (!_createMap.ContainsKey(name))
                {
                    _createMap.Add(name, func);
                }
            }
            return func;
        }

        static Func<ISqlResult, T> ToCreateUseDbResultCore<T>(List<string> getIndexInSelect, Expression exp)
        {
            var newExp = exp as NewExpression;
            if (newExp == null)
            {
                var init = exp as MemberInitExpression;
                if (init != null)
                {
                    newExp = init.NewExpression;
                }
                else
                {
                    var member = exp as MemberExpression;
                    var type = ((PropertyInfo)member.Member).PropertyType;
                    var constructor = type.GetConstructor(new Type[0]);
                    newExp = Expression.New(constructor);
                }
            }
            var param = Expression.Parameter(typeof(ISqlResult), "dbResult");
            var arguments = new[] { param };
            return Expression.Lambda<Func<ISqlResult, T>>(New(getIndexInSelect, new string[0], newExp, param), arguments).Compile();
        }

        static Expression New(List<string> getIndexInSelect, string[] names, NewExpression exp, ParameterExpression param)
        {
            //normal type new.
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
                            Expression.Call(param, typeof(ISqlResult).GetMethod(funcName), Expression.Constant(getIndexInSelect.IndexOf(name)))));
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

            //anonymous type new.
            var newExpression = Expression.New(exp.Constructor, ConvertArguments(getIndexInSelect, names, exp.Arguments.ToArray(), exp.Members.ToArray(), param), exp.Members);
            CacheLambdaChildCreate(getIndexInSelect, names, param, newExpression);
            return newExpression;
        }

        static void CacheLambdaChildCreate(List<string> getIndexInSelect, string[] names, ParameterExpression param, NewExpression newExpression)
        {
            var targetTablePrefix = string.Join(".", names) + ".";
            var nameX = newExpression.Type.FullName + "@" + string.Join("@", getIndexInSelect.Where(e => e.IndexOf(targetTablePrefix) == 0).Select(e => e.Substring(targetTablePrefix.Length)).ToArray());
            lock (_createMap)
            {
                _createMap[nameX] = Expression.Lambda(newExpression, new[] { param }).Compile(); ;
            }
        }

        static IEnumerable<Expression> ConvertArguments(List<string> getIndexInSelect, string[] names, Expression[] args, MemberInfo[] members, ParameterExpression param)
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
                    currentNames = names.Concat(new[] { method.GetPropertyName() }).ToArray();
                }
                var newExp = arg as NewExpression;
                if (newExp == null)
                {
                    if (SupportedTypeSpec.IsSupported(paramType))
                    {
                        var name = string.Join(".", currentNames);
                        newArgs.Add(Expression.Call(param, typeof(ISqlResult).GetMethod("Get" + paramType.Name), 
                            Expression.Constant(getIndexInSelect.IndexOf(name))));
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

        static string GetPropertyName(this MethodInfo method) 
            => (method.Name.IndexOf("get_") == 0) ? method.Name.Substring(4) : method.Name;
    }
}

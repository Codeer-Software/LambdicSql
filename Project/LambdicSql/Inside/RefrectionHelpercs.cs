using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    static class RefrectionHelpercs
    {
        static Dictionary<Type, Func<ISqlStringConverter, MethodCallExpression[], string>> _methodToStrings =
                new Dictionary<Type, Func<ISqlStringConverter, MethodCallExpression[], string>>();

        internal static string GetPropertyName(this MethodInfo method)
            => (method.Name.IndexOf("get_") == 0) ?
                method.Name.Substring(4) : method.Name;

        internal static Func<ISqlStringConverter, MethodCallExpression[], string>
            GetMethodsToString(this MethodInfo method)
        {
            lock (_methodToStrings)
            {
                Func<ISqlStringConverter, MethodCallExpression[], string> func;
                if (_methodToStrings.TryGetValue(method.DeclaringType, out func)) return func;

                var methodToString = method.DeclaringType.GetMethod("MethodsToString");
                var arguments = new[] {
                    Expression.Parameter(typeof(ISqlStringConverter), "cnv"),
                    Expression.Parameter(typeof(MethodCallExpression[]), "exps")
                };
                func = Expression.Lambda<Func<ISqlStringConverter, MethodCallExpression[], string>>
                    (Expression.Call(null, methodToString, arguments), arguments).Compile();
                _methodToStrings.Add(method.DeclaringType, func);
                return func;
            }
        }
    }
}

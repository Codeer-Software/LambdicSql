using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace LambdicSql.Inside
{
    static class RefrectionHelpercs
    {
        static Dictionary<Type, Func<ISqlStringConverter, MethodCallExpression[], string>> _methodToStrings =
                new Dictionary<Type, Func<ISqlStringConverter, MethodCallExpression[], string>>();

        static Dictionary<Type, bool> _isSqlSyntax = new Dictionary<Type, bool>();

        internal static string GetPropertyName(this MethodInfo method)
            => (method.Name.IndexOf("get_") == 0) ?
                method.Name.Substring(4) : method.Name;

        internal static bool IsSqlSyntaxResolver(this MethodInfo method)
        {
            lock (_isSqlSyntax)
            {
                bool check;
                if (!_isSqlSyntax.TryGetValue(method.DeclaringType, out check))
                {
                    check = method.DeclaringType.GetCustomAttributes(true).Any(e=>e is SqlSyntaxAttribute);
                    _isSqlSyntax[method.DeclaringType] = check;
                }
                if (check) return true;
            }

            var ps = method.GetParameters();
            return method.IsStatic &&
                0 < ps.Length &&
                typeof(ISqlSyntax).IsAssignableFrom(ps[0].ParameterType);
        }

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

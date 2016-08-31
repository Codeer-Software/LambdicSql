using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;

namespace LambdicSql.Inside
{
    static class SqlSyntaxHelper
    {
        static Dictionary<Type, Func<ISqlStringConverter, MethodCallExpression[], string>> _methodToStrings =
                new Dictionary<Type, Func<ISqlStringConverter, MethodCallExpression[], string>>();

        static Dictionary<Type, Func<ISqlStringConverter, MemberExpression, string>> _memberToStrings =
                new Dictionary<Type, Func<ISqlStringConverter, MemberExpression, string>>();

        static Dictionary<Type, Func<ISqlStringConverter, NewExpression, string>> _newToStrings =
                new Dictionary<Type, Func<ISqlStringConverter, NewExpression, string>>();

        static Dictionary<Type, bool> _isSqlSyntax = new Dictionary<Type, bool>();

        internal static bool IsSqlSyntax(this Type type)
        {
            lock (_isSqlSyntax)
            {
                bool check;
                if (!_isSqlSyntax.TryGetValue(type, out check))
                {
                    check = type.GetCustomAttributes(true).Any(e=>e is SqlSyntaxAttribute);
                    _isSqlSyntax[type] = check;
                }
                return check;
            }
        }

        internal static Func<ISqlStringConverter, MethodCallExpression[], string>
            GetMethodsToString(this MethodCallExpression exp)
        {
            var type = exp.Method.DeclaringType;
            lock (_methodToStrings)
            {
                Func<ISqlStringConverter, MethodCallExpression[], string> func;
                if (_methodToStrings.TryGetValue(type, out func)) return func;
                
                var methodToString = type.GetMethod("ToString", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                    null,
                    new Type[] { typeof(ISqlStringConverter), typeof(MethodCallExpression[]) },
                    new ParameterModifier[0]);
                var arguments = new[] {
                    Expression.Parameter(typeof(ISqlStringConverter), "cnv"),
                    Expression.Parameter(typeof(MethodCallExpression[]), "exps")
                };

                func = Expression.Lambda<Func<ISqlStringConverter, MethodCallExpression[], string>>
                    (Expression.Call(null, methodToString, arguments), arguments).Compile();
                _methodToStrings.Add(type, func);
                return func;
            }
        }
        
        internal static Func<ISqlStringConverter, MemberExpression, string>
            GetMemberToString(this MemberExpression exp)
        {
            var type = exp.Member.DeclaringType;
            lock (_memberToStrings) 
            {
                Func<ISqlStringConverter, MemberExpression, string> func;
                if (_memberToStrings.TryGetValue(type, out func)) return func;

                var methodToString = type.GetMethod("ToString", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                    null,
                    new Type[] { typeof(ISqlStringConverter), typeof(MemberExpression) },
                    new ParameterModifier[0]);

                var arguments = new[] {
                    Expression.Parameter(typeof(ISqlStringConverter), "cnv"),
                    Expression.Parameter(typeof(MemberExpression), "exps")
                };

                func = Expression.Lambda<Func<ISqlStringConverter, MemberExpression, string>>
                    (Expression.Call(null, methodToString, arguments), arguments).Compile();
                _memberToStrings.Add(type, func);
                return func;
            }
        }

        internal static Func<ISqlStringConverter, NewExpression, string>
            GetNewToString(this NewExpression exp)
        {
            var type = exp.Constructor.DeclaringType;
            lock (_newToStrings)
            {
                Func<ISqlStringConverter, NewExpression, string> func;
                if (_newToStrings.TryGetValue(type, out func)) return func;

                var newToString = type.GetMethod("ToString", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                    null,
                    new Type[] { typeof(ISqlStringConverter), typeof(NewExpression) },
                    new ParameterModifier[0]);

                var arguments = new[] {
                    Expression.Parameter(typeof(ISqlStringConverter), "cnv"),
                    Expression.Parameter(typeof(NewExpression), "exps")
                };
                func = Expression.Lambda<Func<ISqlStringConverter, NewExpression, string>>
                    (Expression.Call(null, newToString, arguments), arguments).Compile();
                _newToStrings.Add(type, func);
                return func;
            }
        }
    }
}

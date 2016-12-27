using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using LambdicSql.SqlBase.TextParts;
using System.Runtime.CompilerServices;

namespace LambdicSql.Inside
{
    static class SqlSyntaxHelper
    {
        const BindingFlags MethodFindFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        static Dictionary<Type, Func<ISqlStringConverter, MethodCallExpression[], SqlText>> _methodToStrings = new Dictionary<Type, Func<ISqlStringConverter, MethodCallExpression[], SqlText>>();
        static Dictionary<Type, Func<ISqlStringConverter, MemberExpression, SqlText>> _memberToStrings = new Dictionary<Type, Func<ISqlStringConverter, MemberExpression, SqlText>>();
        static Dictionary<Type, Func<ISqlStringConverter, NewExpression, SqlText>> _newToStrings = new Dictionary<Type, Func<ISqlStringConverter, NewExpression, SqlText>>();
        static Dictionary<Type, bool> _isSqlSyntax = new Dictionary<Type, bool>();
        static Dictionary<MetaId, bool> _isForcedMethodGroup = new Dictionary<MetaId, bool>();
        static Dictionary<MetaId, bool> _isExtension = new Dictionary<MetaId, bool>();

        internal static bool IsSqlSyntax(this Type type)
        {
            lock (_isSqlSyntax)
            {
                bool check;
                if (!_isSqlSyntax.TryGetValue(type, out check))
                {
                    check = type.GetCustomAttributes(true).Any(e => e is SqlSyntaxAttribute);
                    _isSqlSyntax[type] = check;
                }
                return check;
            }
        }
        
        internal static bool IsForcedMethodGroup(this MethodInfo methodInfo)
        {
            var id = new MetaId(methodInfo);
            lock (_isForcedMethodGroup)
            {
                bool check;
                if (!_isForcedMethodGroup.TryGetValue(id, out check))
                {
                    check = methodInfo.IsDefined(typeof(ForcedMethodGroupAttribute), false);
                    _isForcedMethodGroup[id] = check;
                }
                return check;
            }
        }

        internal static bool IsExtension(this MethodInfo methodInfo)
        {
            var id = new MetaId(methodInfo);
            lock (_isExtension)
            {
                bool check;
                if (!_isExtension.TryGetValue(id, out check))
                {
                    check = methodInfo.IsDefined(typeof(ExtensionAttribute), false);
                    _isExtension[id] = check;
                }
                return check;
            }
        }
        
        internal static Func<ISqlStringConverter, MethodCallExpression[], SqlText> GetConverotrMethod(this MethodCallExpression exp)
        {
            var type = exp.Method.DeclaringType;
            lock (_methodToStrings)
            {
                Func<ISqlStringConverter, MethodCallExpression[], SqlText> func;
                if (_methodToStrings.TryGetValue(type, out func)) return func;
                
                var methodToString = type.GetMethod("Convert", MethodFindFlags,
                    null,
                    new Type[] { typeof(ISqlStringConverter), typeof(MethodCallExpression[]) },
                    new ParameterModifier[0]);

                var arguments = new[] {
                    Expression.Parameter(typeof(ISqlStringConverter), "cnv"),
                    Expression.Parameter(typeof(MethodCallExpression[]), "exps")
                };

                func = Expression.Lambda<Func<ISqlStringConverter, MethodCallExpression[], SqlText>>
                    (Expression.Call(null, methodToString, arguments), arguments).Compile();

                _methodToStrings.Add(type, func);

                return func;
            }
        }
        
        internal static Func<ISqlStringConverter, MemberExpression, SqlText> GetConverotrMethod(this MemberExpression exp)
        {
            var type = exp.Member.DeclaringType;
            lock (_memberToStrings) 
            {
                Func<ISqlStringConverter, MemberExpression, SqlText> func;
                if (_memberToStrings.TryGetValue(type, out func)) return func;

                var methodToString = type.GetMethod("Convert", MethodFindFlags,
                    null,
                    new Type[] { typeof(ISqlStringConverter), typeof(MemberExpression) },
                    new ParameterModifier[0]);

                var arguments = new[] {
                    Expression.Parameter(typeof(ISqlStringConverter), "cnv"),
                    Expression.Parameter(typeof(MemberExpression), "exps")
                };

                func = Expression.Lambda<Func<ISqlStringConverter, MemberExpression, SqlText>>
                    (Expression.Call(null, methodToString, arguments), arguments).Compile();

                _memberToStrings.Add(type, func);

                return func;
            }
        }

        internal static Func<ISqlStringConverter, NewExpression, SqlText> GetConverotrMethod(this NewExpression exp)
        {
            var type = exp.Constructor.DeclaringType;
            lock (_newToStrings)
            {
                Func<ISqlStringConverter, NewExpression, SqlText> func;
                if (_newToStrings.TryGetValue(type, out func)) return func;

                var newToString = type.GetMethod("Convert", MethodFindFlags,
                    null,
                    new Type[] { typeof(ISqlStringConverter), typeof(NewExpression) },
                    new ParameterModifier[0]);

                var arguments = new[] {
                    Expression.Parameter(typeof(ISqlStringConverter), "cnv"),
                    Expression.Parameter(typeof(NewExpression), "exps")
                };

                func = Expression.Lambda<Func<ISqlStringConverter, NewExpression, SqlText>>
                    (Expression.Call(null, newToString, arguments), arguments).Compile();
                _newToStrings.Add(type, func);

                return func;
            }
        }
        
        internal static int SkipMethodChain(this MethodCallExpression exp, int index)
        {
            var ps = exp.Method.GetParameters();
            if (0 < ps.Length && typeof(IMethodChain).IsAssignableFrom(ps[0].ParameterType)) return index + 1;
            else return index;
        }
    }
}

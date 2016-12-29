﻿using LambdicSql.SqlBase;
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

        static Dictionary<Type, Func<IExpressionConverter, MemberExpression, ExpressionElement>> _memberToStrings = new Dictionary<Type, Func<IExpressionConverter, MemberExpression, ExpressionElement>>();
        static Dictionary<Type, Func<IExpressionConverter, NewExpression, ExpressionElement>> _newToStrings = new Dictionary<Type, Func<IExpressionConverter, NewExpression, ExpressionElement>>();

        static Dictionary<MetaId, bool> _isForcedMethodGroup = new Dictionary<MetaId, bool>();
        static Dictionary<MetaId, bool> _isExtension = new Dictionary<MetaId, bool>();
        static Dictionary<MetaId, SqlSyntaxMethodAttribute> _sqlSyntaxMethodAttribute = new Dictionary<MetaId, SqlSyntaxMethodAttribute>();
        static Dictionary<MetaId, SqlSyntaxMemberAttribute> _sqlSyntaxMemberAttribute = new Dictionary<MetaId, SqlSyntaxMemberAttribute>();
        static Dictionary<MetaId, SqlSyntaxNewAttribute> _sqlSyntaxNewAttribute = new Dictionary<MetaId, SqlSyntaxNewAttribute>();
        static Dictionary<Type, SqlSyntaxObjectAttribute> _sqlSyntaxObjectAttribute = new Dictionary<Type, SqlSyntaxObjectAttribute>();

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

        internal static SqlSyntaxMethodAttribute GetSqlSyntaxMethod(this MethodCallExpression exp)
        {
            var methodInfo = exp.Method;
            var id = new MetaId(methodInfo);
            lock (_sqlSyntaxMethodAttribute)
            {
                SqlSyntaxMethodAttribute attr;
                if (_sqlSyntaxMethodAttribute.TryGetValue(id, out attr)) return attr;

                var attrs = methodInfo.GetCustomAttributes(typeof(SqlSyntaxMethodAttribute), true);
                if (attrs.Length == 1) attr = attrs[0] as SqlSyntaxMethodAttribute;
                else attr = null;
                _sqlSyntaxMethodAttribute.Add(id, attr);
                return attr;
            }
        }
        
        internal static SqlSyntaxMemberAttribute GetSqlSyntaxMember(this MemberExpression exp)
        {
            var member = exp.Member;
            var id = new MetaId(member);
            lock (_sqlSyntaxMemberAttribute)
            {
                SqlSyntaxMemberAttribute attr;
                if (_sqlSyntaxMemberAttribute.TryGetValue(id, out attr)) return attr;

                var attrs = member.GetCustomAttributes(typeof(SqlSyntaxMemberAttribute), true);
                if (attrs.Length == 1) attr = attrs[0] as SqlSyntaxMemberAttribute;
                else attr = null;
                _sqlSyntaxMemberAttribute.Add(id, attr);
                return attr;
            }
        }


        internal static SqlSyntaxNewAttribute GetSqlSyntaxNew(this NewExpression exp)
        {
            var constructor = exp.Constructor;
            var id = new MetaId(constructor);
            lock (_sqlSyntaxNewAttribute)
            {
                SqlSyntaxNewAttribute attr;
                if (_sqlSyntaxNewAttribute.TryGetValue(id, out attr)) return attr;

                var attrs = constructor.GetCustomAttributes(typeof(SqlSyntaxNewAttribute), true);
                if (attrs.Length == 1) attr = attrs[0] as SqlSyntaxNewAttribute;
                else attr = null;
                _sqlSyntaxNewAttribute.Add(id, attr);
                return attr;
            }
        }

        internal static SqlSyntaxObjectAttribute GetSqlSyntaxObject(this Type type)
        {
            lock (_sqlSyntaxObjectAttribute)
            {
                SqlSyntaxObjectAttribute attr;
                if (_sqlSyntaxObjectAttribute.TryGetValue(type, out attr)) return attr;

                var attrs = type.GetCustomAttributes(typeof(SqlSyntaxObjectAttribute), true);
                if (attrs.Length == 1) attr = attrs[0] as SqlSyntaxObjectAttribute;
                else attr = null;
                _sqlSyntaxObjectAttribute.Add(type, attr);
                return attr;
            }
        }


        internal static Func<IExpressionConverter, MemberExpression, ExpressionElement> GetConverotrMethod(this MemberExpression exp)
        {
            var type = exp.Member.DeclaringType;
            lock (_memberToStrings) 
            {
                Func<IExpressionConverter, MemberExpression, ExpressionElement> func;
                if (_memberToStrings.TryGetValue(type, out func)) return func;

                var methodToString = type.GetMethod("Convert", MethodFindFlags,
                    null,
                    new Type[] { typeof(IExpressionConverter), typeof(MemberExpression) },
                    new ParameterModifier[0]);

                var arguments = new[] {
                    Expression.Parameter(typeof(IExpressionConverter), "cnv"),
                    Expression.Parameter(typeof(MemberExpression), "exps")
                };

                func = Expression.Lambda<Func<IExpressionConverter, MemberExpression, ExpressionElement>>
                    (Expression.Call(null, methodToString, arguments), arguments).Compile();

                _memberToStrings.Add(type, func);

                return func;
            }
        }

        internal static Func<IExpressionConverter, NewExpression, ExpressionElement> GetConverotrMethod(this NewExpression exp)
        {
            var type = exp.Constructor.DeclaringType;
            lock (_newToStrings)
            {
                Func<IExpressionConverter, NewExpression, ExpressionElement> func;
                if (_newToStrings.TryGetValue(type, out func)) return func;

                var newToString = type.GetMethod("Convert", MethodFindFlags,
                    null,
                    new Type[] { typeof(IExpressionConverter), typeof(NewExpression) },
                    new ParameterModifier[0]);

                var arguments = new[] {
                    Expression.Parameter(typeof(IExpressionConverter), "cnv"),
                    Expression.Parameter(typeof(NewExpression), "exps")
                };

                func = Expression.Lambda<Func<IExpressionConverter, NewExpression, ExpressionElement>>
                    (Expression.Call(null, newToString, arguments), arguments).Compile();
                _newToStrings.Add(type, func);

                return func;
            }
        }
        
        internal static int SkipMethodChain(this MethodCallExpression exp, int index)
        {
            var ps = exp.Method.GetParameters();
            if (0 < ps.Length && typeof(IClauseChain).IsAssignableFrom(ps[0].ParameterType)) return index + 1;
            else return index;
        }
    }
}

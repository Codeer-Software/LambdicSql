using LambdicSql.ExpressionConverterService.SqlSyntaxConverter;
using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LambdicSql.Inside
{
    static class SqlSyntaxHelper
    {
        static Dictionary<MetaId, bool> _isExtension = new Dictionary<MetaId, bool>();
        static Dictionary<MetaId, SqlSyntaxConverterMethodAttribute> _sqlSyntaxMethodAttribute = new Dictionary<MetaId, SqlSyntaxConverterMethodAttribute>();
        static Dictionary<MetaId, SqlSyntaxConverterMemberAttribute> _sqlSyntaxMemberAttribute = new Dictionary<MetaId, SqlSyntaxConverterMemberAttribute>();
        static Dictionary<MetaId, SqlSyntaxConverterNewAttribute> _sqlSyntaxNewAttribute = new Dictionary<MetaId, SqlSyntaxConverterNewAttribute>();
        static Dictionary<Type, SqlSyntaxConverterObjectAttribute> _sqlSyntaxObjectAttribute = new Dictionary<Type, SqlSyntaxConverterObjectAttribute>();
        
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

        internal static SqlSyntaxConverterObjectAttribute GetSqlSyntaxObject(this Type type)
        {
            lock (_sqlSyntaxObjectAttribute)
            {
                SqlSyntaxConverterObjectAttribute attr;
                if (_sqlSyntaxObjectAttribute.TryGetValue(type, out attr)) return attr;

                var attrs = type.GetCustomAttributes(typeof(SqlSyntaxConverterObjectAttribute), true);
                if (attrs.Length == 1) attr = attrs[0] as SqlSyntaxConverterObjectAttribute;
                else attr = null;
                _sqlSyntaxObjectAttribute.Add(type, attr);
                return attr;
            }
        }

        internal static SqlSyntaxConverterMethodAttribute GetSqlSyntaxMethod(this MethodCallExpression exp)
            => GetSqlSyntaxMember(exp.Method, _sqlSyntaxMethodAttribute);
        
        internal static SqlSyntaxConverterMemberAttribute GetSqlSyntaxMember(this MemberExpression exp)
            => GetSqlSyntaxMember(exp.Member, _sqlSyntaxMemberAttribute);

        internal static SqlSyntaxConverterNewAttribute GetSqlSyntaxNew(this NewExpression exp)
            => GetSqlSyntaxMember(exp.Constructor, _sqlSyntaxNewAttribute);

        static T GetSqlSyntaxMember<T>(MemberInfo member, Dictionary<MetaId, T> cache) where T : class
        {
            var id = new MetaId(member);
            lock (cache)
            {
                T attr;
                if (cache.TryGetValue(id, out attr)) return attr;

                var attrs = member.GetCustomAttributes(typeof(T), true);
                if (attrs.Length == 1) attr = attrs[0] as T;
                else attr = null;
                cache.Add(id, attr);
                return attr;
            }
        }

        internal static int SkipMethodChain(this MethodCallExpression exp, int index)
        {
            if (!exp.Method.IsExtension()) return index;

            var ps = exp.Method.GetParameters();
            if (0 < ps.Length && typeof(IClauseChain).IsAssignableFrom(ps[0].ParameterType)) return index + 1;
            else return index;
        }
    }
}

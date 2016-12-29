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
        static Dictionary<MetaId, SqlSyntaxAttribute> _sqlSyntaxMethodAttribute = new Dictionary<MetaId, SqlSyntaxAttribute>();
        static Dictionary<MetaId, SqlSyntaxAttribute> _sqlSyntaxMemberAttribute = new Dictionary<MetaId, SqlSyntaxAttribute>();
        static Dictionary<MetaId, SqlSyntaxAttribute> _sqlSyntaxNewAttribute = new Dictionary<MetaId, SqlSyntaxAttribute>();
        static Dictionary<Type, SqlSyntaxAttribute> _sqlSyntaxObjectAttribute = new Dictionary<Type, SqlSyntaxAttribute>();
        
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

        internal static SqlSyntaxAttribute GetSqlSyntaxObject(this Type type)
        {
            lock (_sqlSyntaxObjectAttribute)
            {
                SqlSyntaxAttribute attr;
                if (_sqlSyntaxObjectAttribute.TryGetValue(type, out attr)) return attr;

                var attrs = type.GetCustomAttributes(typeof(SqlSyntaxAttribute), true);
                if (attrs.Length == 1) attr = attrs[0] as SqlSyntaxAttribute;
                else attr = null;
                _sqlSyntaxObjectAttribute.Add(type, attr);
                return attr;
            }
        }

        internal static SqlSyntaxAttribute GetSqlSyntaxMethod(this MethodCallExpression exp)
            => GetSqlSyntaxMember(exp.Method, _sqlSyntaxMethodAttribute);
        
        internal static SqlSyntaxAttribute GetSqlSyntaxMember(this MemberExpression exp)
            => GetSqlSyntaxMember(exp.Member, _sqlSyntaxMemberAttribute);

        internal static SqlSyntaxAttribute GetSqlSyntaxNew(this NewExpression exp)
            => GetSqlSyntaxMember(exp.Constructor, _sqlSyntaxNewAttribute);

        static SqlSyntaxAttribute GetSqlSyntaxMember(MemberInfo member, Dictionary<MetaId, SqlSyntaxAttribute> cache)
        {
            var id = new MetaId(member);
            lock (cache)
            {
                SqlSyntaxAttribute attr;
                if (cache.TryGetValue(id, out attr)) return attr;

                var attrs = member.GetCustomAttributes(typeof(SqlSyntaxAttribute), true);
                if (attrs.Length == 1) attr = attrs[0] as SqlSyntaxAttribute;
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

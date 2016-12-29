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
        static Dictionary<MetaId, SqlSyntaxConverterAttribute> _sqlSyntaxMethodAttribute = new Dictionary<MetaId, SqlSyntaxConverterAttribute>();
        static Dictionary<MetaId, SqlSyntaxConverterAttribute> _sqlSyntaxMemberAttribute = new Dictionary<MetaId, SqlSyntaxConverterAttribute>();
        static Dictionary<MetaId, SqlSyntaxConverterAttribute> _sqlSyntaxNewAttribute = new Dictionary<MetaId, SqlSyntaxConverterAttribute>();
        static Dictionary<Type, SqlSyntaxConverterAttribute> _sqlSyntaxObjectAttribute = new Dictionary<Type, SqlSyntaxConverterAttribute>();
        
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

        internal static SqlSyntaxConverterAttribute GetSqlSyntaxObject(this Type type)
        {
            lock (_sqlSyntaxObjectAttribute)
            {
                SqlSyntaxConverterAttribute attr;
                if (_sqlSyntaxObjectAttribute.TryGetValue(type, out attr)) return attr;

                var attrs = type.GetCustomAttributes(typeof(SqlSyntaxConverterAttribute), true);
                if (attrs.Length == 1) attr = attrs[0] as SqlSyntaxConverterAttribute;
                else attr = null;
                _sqlSyntaxObjectAttribute.Add(type, attr);
                return attr;
            }
        }

        internal static SqlSyntaxConverterAttribute GetSqlSyntaxMethod(this MethodCallExpression exp)
            => GetSqlSyntaxMember(exp.Method, _sqlSyntaxMethodAttribute);
        
        internal static SqlSyntaxConverterAttribute GetSqlSyntaxMember(this MemberExpression exp)
            => GetSqlSyntaxMember(exp.Member, _sqlSyntaxMemberAttribute);

        internal static SqlSyntaxConverterAttribute GetSqlSyntaxNew(this NewExpression exp)
            => GetSqlSyntaxMember(exp.Constructor, _sqlSyntaxNewAttribute);

        static SqlSyntaxConverterAttribute GetSqlSyntaxMember(MemberInfo member, Dictionary<MetaId, SqlSyntaxConverterAttribute> cache)
        {
            var id = new MetaId(member);
            lock (cache)
            {
                SqlSyntaxConverterAttribute attr;
                if (cache.TryGetValue(id, out attr)) return attr;

                var attrs = member.GetCustomAttributes(typeof(SqlSyntaxConverterAttribute), true);
                if (attrs.Length == 1) attr = attrs[0] as SqlSyntaxConverterAttribute;
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

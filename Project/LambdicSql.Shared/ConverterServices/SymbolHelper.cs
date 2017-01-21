using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.MultiplatformCompatibe;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LambdicSql.ConverterServices.Inside
{
    static class SymbolHelper
    {
        static Dictionary<MetaId, bool> _isExtension = new Dictionary<MetaId, bool>();
        static Dictionary<MetaId, MethodConverterAttribute> _converterMethodAttribute = new Dictionary<MetaId, MethodConverterAttribute>();
        static Dictionary<MetaId, MemberConverterAttribute> _converterMemberAttribute = new Dictionary<MetaId, MemberConverterAttribute>();
        static Dictionary<MetaId, NewConverterAttribute> _converterNewAttribute = new Dictionary<MetaId, NewConverterAttribute>();
        static Dictionary<Type, ObjectConverterAttribute> _converterObjectAttribute = new Dictionary<Type, ObjectConverterAttribute>();
        
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

        internal static ObjectConverterAttribute GetObjectConverter(this Type type)
        {
            lock (_converterObjectAttribute)
            {
                ObjectConverterAttribute attr;
                if (_converterObjectAttribute.TryGetValue(type, out attr)) return attr;

                attr = type.GetAttribute<ObjectConverterAttribute>();
                _converterObjectAttribute.Add(type, attr);
                return attr;
            }
        }

        internal static MethodConverterAttribute GetMethodConverter(this MethodCallExpression exp)
            => GetMemberConverter(exp.Method, _converterMethodAttribute);
        
        internal static MemberConverterAttribute GetMemberConverter(this MemberExpression exp)
            => GetMemberConverter(exp.Member, _converterMemberAttribute);

        internal static NewConverterAttribute GetNewConverter(this NewExpression exp)
            => GetMemberConverter(exp.Constructor, _converterNewAttribute);

        static T GetMemberConverter<T>(MemberInfo member, Dictionary<MetaId, T> cache) where T : class
        {
            var id = new MetaId(member);
            lock (cache)
            {
                T attr;
                if (cache.TryGetValue(id, out attr)) return attr;

                attr = member.GetAttribute<T>();
                cache.Add(id, attr);
                return attr;
            }
        }
        
        internal static int SkipMethodChain(this MethodCallExpression exp, int index)
        {
            if (!exp.Method.IsExtension()) return index;

            var ps = exp.Method.GetParameters();
            if (0 < ps.Length && typeof(IMethodChain).IsAssignableFromEx(ps[0].ParameterType)) return index + 1;
            else return index;
        }
    }
}

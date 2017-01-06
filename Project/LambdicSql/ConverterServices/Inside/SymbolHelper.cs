﻿using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
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
        static Dictionary<MetaId, SymbolConverterMethodAttribute> _converterMethodAttribute = new Dictionary<MetaId, SymbolConverterMethodAttribute>();
        static Dictionary<MetaId, SymbolConverterMemberAttribute> _converterMemberAttribute = new Dictionary<MetaId, SymbolConverterMemberAttribute>();
        static Dictionary<MetaId, SymbolConverterNewAttribute> _converterNewAttribute = new Dictionary<MetaId, SymbolConverterNewAttribute>();
        static Dictionary<Type, SymbolConverterObjectAttribute> _converterObjectAttribute = new Dictionary<Type, SymbolConverterObjectAttribute>();
        
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

        internal static SymbolConverterObjectAttribute GetObjectConverter(this Type type)
        {
            lock (_converterObjectAttribute)
            {
                SymbolConverterObjectAttribute attr;
                if (_converterObjectAttribute.TryGetValue(type, out attr)) return attr;

                var attrs = type.GetCustomAttributes(typeof(SymbolConverterObjectAttribute), true);
                if (attrs.Length == 1) attr = attrs[0] as SymbolConverterObjectAttribute;
                else attr = null;
                _converterObjectAttribute.Add(type, attr);
                return attr;
            }
        }

        internal static SymbolConverterMethodAttribute GetMethodConverter(this MethodCallExpression exp)
            => GetMemberConverter(exp.Method, _converterMethodAttribute);
        
        internal static SymbolConverterMemberAttribute GetMemberConverter(this MemberExpression exp)
            => GetMemberConverter(exp.Member, _converterMemberAttribute);

        internal static SymbolConverterNewAttribute GetNewConverter(this NewExpression exp)
            => GetMemberConverter(exp.Constructor, _converterNewAttribute);

        static T GetMemberConverter<T>(MemberInfo member, Dictionary<MetaId, T> cache) where T : class
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

        //TODO これは公開してもいいかも
        internal static int SkipMethodChain(this MethodCallExpression exp, int index)
        {
            if (!exp.Method.IsExtension()) return index;

            var ps = exp.Method.GetParameters();
            if (0 < ps.Length && typeof(IMethodChain).IsAssignableFrom(ps[0].ParameterType)) return index + 1;
            else return index;
        }
    }
}

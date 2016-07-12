using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    static class ExpressionToObject
    {
        static Dictionary<string, Delegate> _memberGet = new Dictionary<string, Delegate>();

        internal static bool GetMemberObject(MemberExpression member, out object obj)
        {
            obj = null;
            var m = member;
            var ns = new List<string>();
            ConstantExpression constant = null;
            while (m != null)
            {
                ns.Add(m.Member.Name);
                constant = m.Expression as ConstantExpression;
                if (constant != null)
                {
                    break;
                }
                m = m.Expression as MemberExpression;
            }
            if (constant == null)
            {
                return false;
            }

            var getterName = constant.Type.FullName + "@" + string.Join("@", ns.ToArray());
            Delegate getFunc;
            if (!_memberGet.TryGetValue(getterName, out getFunc))
            {
                ns.Reverse();

                var param = Expression.Parameter(constant.Type, "param");
                Expression p = param;
                for (int i = 0; i < ns.Count; i++)
                {
                    p = Expression.PropertyOrField(p, ns[i]);
                }

                var f = Expression.Lambda(Expression.Convert(p, typeof(object)), new[] { param }).Compile();
                _memberGet.Add(getterName, f);
                getFunc = f;
            }
            obj = getFunc.DynamicInvoke(constant.Value);
            return true;
        }
    }
}

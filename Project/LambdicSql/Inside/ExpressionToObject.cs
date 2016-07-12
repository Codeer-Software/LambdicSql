using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    static class ExpressionToObject
    {
        static Dictionary<string, Delegate> _memberGet = new Dictionary<string, Delegate>();

        internal static bool GetMemberObject(MemberExpression exp, out object obj)
        {
            obj = null;
            var member = exp;
            var names = new List<string>();
            ConstantExpression constant = null;
            while (member != null)
            {
                names.Add(member.Member.Name);
                constant = member.Expression as ConstantExpression;
                if (constant != null)
                {
                    break;
                }
                member = member.Expression as MemberExpression;
            }
            if (constant == null)
            {
                return false;
            }

            var getterName = constant.Type.FullName + "@" + string.Join("@", names.ToArray());
            Delegate getFunc;
            lock (_memberGet)
            {
                if (_memberGet.TryGetValue(getterName, out getFunc))
                {
                    obj = getFunc.DynamicInvoke(constant.Value);
                    return true;
                }
            }

            var param = Expression.Parameter(constant.Type, "param");
            Expression target = param;
            names.Reverse();
            names.ForEach(e => target = Expression.PropertyOrField(target, e));

            getFunc = Expression.Lambda(Expression.Convert(target, typeof(object)), new[] { param }).Compile();
            lock (_memberGet)
            {
                _memberGet.Add(getterName, getFunc);
            }
            obj = getFunc.DynamicInvoke(constant.Value);
            return true;
        }
    }
}

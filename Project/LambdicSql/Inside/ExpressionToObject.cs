using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    static class ExpressionToObject
    {
        static Dictionary<string, IGetter> _memberGet = new Dictionary<string, IGetter>();

        internal static bool GetExpressionObject(Expression exp, out object obj)
        {
            var constExp = exp as ConstantExpression;
            if (constExp != null)
            {
                obj = constExp.Value;
                return true;
            }
            return GetMemberObject(exp as MemberExpression, out obj);
        }

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
            IGetter getter;
            lock (_memberGet)
            {
                if (_memberGet.TryGetValue(getterName, out getter))
                {
                    obj = getter.GetMemberObject(constant.Value);
                    return true;
                }
            }

            var param = Expression.Parameter(constant.Type, "param");
            Expression target = param;
            names.Reverse();
            names.ForEach(e => target = Expression.PropertyOrField(target, e));
            getter = Activator.CreateInstance(typeof(GetterCore<>).MakeGenericType(constant.Type), true) as IGetter;
            getter.Init(Expression.Convert(target, typeof(object)), param);
            lock (_memberGet)
            {
                if (!_memberGet.ContainsKey(getterName))
                {
                    _memberGet.Add(getterName, getter);
                }
            }
            obj = getter.GetMemberObject(constant.Value);
            return true;
        }

        interface IGetter
        {
            void Init(Expression exp, ParameterExpression param);
            object GetMemberObject(object target);
        }

        class GetterCore<T> : IGetter
        {
            Func<T, object> _func;

            public void Init(Expression exp, ParameterExpression param)
            {
                _func = Expression.Lambda<Func<T, object>>(exp, new[] { param }).Compile();
            }

            public object GetMemberObject(object target) => _func((T)target);
        }
    }
}

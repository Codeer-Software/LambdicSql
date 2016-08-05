using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.Inside
{
    static partial class ExpressionToObject
    {
        interface IGetter
        {
            void Init(Expression exp, ParameterExpression[] param);
            object GetMemberObject(object[] arguments);
        }

        static Dictionary<string, IGetter> _memberGet = new Dictionary<string, IGetter>();

        internal static bool GetExpressionObject(Expression exp, out object obj)
        {
            var constExp = exp as ConstantExpression;
            if (constExp != null)
            {
                obj = constExp.Value;
                return true;
            }
            var member = exp as MemberExpression;
            if (member != null)
            {
                return GetMemberObject(member, out obj);
            }
            var method = exp as MethodCallExpression;
            if (method != null)
            {
                return GetMethodObject(method, out obj);
            }
            obj = null;
            return false;
        }

        internal static bool GetMethodObject(MethodCallExpression method, out object value)
        {
            value = null;

            if (method.Method.ReturnType == typeof(void))
            {
                throw new NotSupportedException("Can't call void method.");
            }

            //instance.
            object instance = null;
            GetExpressionObject(method.Object, out instance);

            //arguments.
            var ps = method.Method.GetParameters().Select(e=>e.ParameterType).ToList();
            var psExp = new List<ParameterExpression>();
            var args = new List<object>();
            for (int i = 0; i < ps.Count; i++)
            {
                psExp.Add(Expression.Parameter(ps[i], "p" + i));
                object arg;
                GetExpressionObject(method.Arguments[i], out arg);
                args.Add(arg);
            }
            if (instance != null)
            {
                args.Add(instance);
            }

            //name.
            var getterName = method.Method.DeclaringType.FullName + "." + method.Method.Name +
                "(" + string.Join(",", ps.Select(e=>e.FullName).ToArray()) + ")";

            //getter.
            IGetter getter;
            lock (_memberGet)
            {
                if (!_memberGet.TryGetValue(getterName, out getter))
                {
                    Expression body = null;
                    if (instance == null)
                    {
                        body = Expression.Convert(Expression.Call(null, method.Method, psExp.ToArray()), typeof(object));
                    }
                    else
                    {
                        var instanceExp = Expression.Parameter(instance.GetType(), "Instance");
                        body = Expression.Convert(Expression.Call(instanceExp, method.Method, psExp.ToArray()), typeof(object));
                        psExp.Add(instanceExp);
                        ps.Add(instance.GetType());
                    }
                    getter = CreateGetter(ps.ToArray());
                    getter.Init(body, psExp.ToArray());
                    _memberGet[getterName] = getter;
                }
            }
            value = getter.GetMemberObject(args.ToArray());
            return true;
        }
        
        internal static bool GetMemberObject(MemberExpression exp, out object value)
        {
            value = null;
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
                    value = getter.GetMemberObject(new object[] { constant.Value });
                    return true;
                }

                var param = Expression.Parameter(constant.Type, "param");
                Expression target = param;
                names.Reverse();
                names.ForEach(e => target = Expression.PropertyOrField(target, e));
                getter = Activator.CreateInstance(typeof(GetterCore<>).MakeGenericType(constant.Type), true) as IGetter;
                getter.Init(Expression.Convert(target, typeof(object)), new[] { param });
                _memberGet.Add(getterName, getter);
            }
            value = getter.GetMemberObject(new object[] { constant.Value });
            return true;
        }
    }
}

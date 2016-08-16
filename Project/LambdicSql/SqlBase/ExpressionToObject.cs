using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.SqlBase
{
    //TODO refactoring.
    public static partial class ExpressionToObject
    {
        interface IGetter
        {
            void Init(Expression exp, ParameterExpression[] param);
            object GetMemberObject(object[] arguments);
        }

        static Dictionary<string, IGetter> _memberGet = new Dictionary<string, IGetter>();

        public static bool GetExpressionObject(Expression exp, out object obj)
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
            var newExp = exp as NewExpression;
            if (newExp != null)
            {
                return GetNewObject(newExp, out obj);
            }
            var memberInit = exp as MemberInitExpression;
            if (memberInit != null)
            {
                return GetMemberInitObject(memberInit, out obj);
            }

            obj = null;
            return false;
        }

        //TODO あー、newとMemberInitが混ざったやつとか、入れ子とかかなり面倒なのでは？
        public static bool GetMemberInitObject(MemberInitExpression memberInit, out object value)
        {
            value = null;
            var srcs = memberInit.Bindings.Select(e => ((MemberAssignment)e).Expression).ToList();
            var ps = srcs.Select(e => e.Type).ToList();
            var psExp = ps.Select((e, i) => Expression.Parameter(e, "p" + i)).ToArray();

            var bs = memberInit.Bindings.Select((e, i) => Expression.Bind(e.Member, psExp[i])).ToArray();
            var args = new List<object>();
            for (int i = 0; i < ps.Count; i++)
            {
                object arg;
                GetExpressionObject(srcs[i], out arg);
                args.Add(arg);
            }

            //name.
            var getterName = memberInit.NewExpression.Type.FullName +
                "()(" + string.Join(",", ps.Select(e => e.FullName).ToArray()) + ")";

            //getter.
            IGetter getter;
            lock (_memberGet)
            {
                if (!_memberGet.TryGetValue(getterName, out getter))
                {
                    Expression body = null;
                    body = Expression.Convert(Expression.MemberInit(memberInit.NewExpression, bs), typeof(object));
                    getter = CreateGetter(ps.ToArray());
                    getter.Init(body, psExp);
                    _memberGet[getterName] = getter;
                }
            }
            value = getter.GetMemberObject(args.ToArray());
            return true;
        }

        public static bool GetNewObject(NewExpression newExp, out object value)
        {
            value = null;

            //arguments.
            var ps = newExp.Constructor.GetParameters().Select(e => e.ParameterType).ToList();
            var psExp = new List<ParameterExpression>();
            var args = new List<object>();
            for (int i = 0; i < ps.Count; i++)
            {
                psExp.Add(Expression.Parameter(ps[i], "p" + i));
                object arg;
                GetExpressionObject(newExp.Arguments[i], out arg);
                args.Add(arg);
            }

            //name.
            var getterName = newExp.Type.FullName + 
                "(" + string.Join(",", ps.Select(e => e.FullName).ToArray()) + ")";

            //getter.
            IGetter getter;
            lock (_memberGet)
            {
                if (!_memberGet.TryGetValue(getterName, out getter))
                {
                    Expression body = null;
                    body = Expression.Convert(Expression.New(newExp.Constructor, psExp.ToArray()), typeof(object));
                    getter = CreateGetter(ps.ToArray());
                    getter.Init(body, psExp.ToArray());
                    _memberGet[getterName] = getter;
                }
            }
            value = getter.GetMemberObject(args.ToArray());
            return true;
        }

        public static bool GetMethodObject(MethodCallExpression method, out object value)
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

        public static bool GetMemberObject(MemberExpression exp, out object value)
        {
            value = null;
            var member = exp;
            var names = new List<string>();
            object targt = null;
            Type type = null;
            while (member != null)
            {
                names.Add(member.Member.Name);
                var constant = member.Expression as ConstantExpression;
                if (constant != null)
                {
                    targt = constant.Value;
                    type = constant.Type;
                    break;
                }
                var method = member.Expression as MethodCallExpression;
                if (method != null)
                {
                    type = method.Type;
                    if (!GetMethodObject(method, out targt)) return false;
                }
                member = member.Expression as MemberExpression;
            }
            if (targt == null)
            {
                return false;
            }

            var getterName = type.FullName + "@" + string.Join("@", names.ToArray());
            IGetter getter;
            lock (_memberGet)
            {
                if (_memberGet.TryGetValue(getterName, out getter))
                {
                    value = getter.GetMemberObject(new object[] { targt });
                    return true;
                }

                var param = Expression.Parameter(type, "param");
                Expression target = param;
                names.Reverse();
                names.ForEach(e => target = Expression.PropertyOrField(target, e));
                getter = Activator.CreateInstance(typeof(GetterCore<>).MakeGenericType(type), true) as IGetter;
                getter.Init(Expression.Convert(target, typeof(object)), new[] { param });
                _memberGet.Add(getterName, getter);
            }
            value = getter.GetMemberObject(new object[] { targt });
            return true;
        }
    }
}

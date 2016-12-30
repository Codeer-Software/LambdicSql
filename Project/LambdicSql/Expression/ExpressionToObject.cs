using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;

namespace LambdicSql.Inside
{
    internal static partial class ExpressionToObject
    {
        interface IGetter
        {
            void Init(System.Linq.Expressions.Expression exp, ParameterExpression[] param);
            object GetMemberObject(object[] arguments);
        }

        static Dictionary<string, IGetter> _memberGet = new Dictionary<string, IGetter>();

        internal static bool GetExpressionObject(System.Linq.Expressions.Expression exp, out object obj)
        {
            bool not = false;
            while (true)
            {
                var unary = exp as UnaryExpression;
                if (unary == null) break;

                if (unary.NodeType == ExpressionType.Not) not = !not;

                exp = unary.Operand;
            }

            var ret = GetExpressionObjectCore(exp, out obj);
            if (not) obj = !((bool)obj);
            return ret;
        }

        static bool GetExpressionObjectCore(System.Linq.Expressions.Expression exp, out object obj)
        {
            obj = null;

            var binaryExp = exp as BinaryExpression;
            if (binaryExp != null)
            {
                obj = GetBinaryExpression(binaryExp);
                return true;
            }

            var constExp = exp as ConstantExpression;
            if (constExp != null)
            {
                obj = constExp.Value;
                return true;
            }

            var member = exp as MemberExpression;
            if (member != null)
            {
                obj = GetMemberObject(member);
                return true;
            }

            var method = exp as MethodCallExpression;
            if (method != null)
            {
                obj = GetMethodObject(method);
                return true;
            }

            var newExp = exp as NewExpression;
            if (newExp != null)
            {
                obj = GetNewObject(newExp);
                return true;
            }

            var memberInit = exp as MemberInitExpression;
            if (memberInit != null)
            {
                obj = GetMemberInitObject(memberInit);
                return true;
            }

            return false;
        }

        static object GetBinaryExpression(BinaryExpression binaryExp)
        {
            object objLeft, objRight;
            if (!GetExpressionObject(binaryExp.Left, out objLeft) ||
                !GetExpressionObject(binaryExp.Right, out objRight))
            {
                throw new NotSupportedException();
            }

            //arguments.
            var left = System.Linq.Expressions.Expression.Parameter(binaryExp.Left.Type, "left");
            var right = System.Linq.Expressions.Expression.Parameter(binaryExp.Right.Type, "right");
            var args = new List<object>();
            args.Add(objLeft);
            args.Add(objRight);

            //name.
            var getterName = binaryExp.Left.Type.FullName + " " + binaryExp.NodeType + " " + binaryExp.Right.Type.FullName;

            //getter.
            IGetter getter;
            lock (_memberGet)
            {
                if (!_memberGet.TryGetValue(getterName, out getter))
                {
                    System.Linq.Expressions.Expression body = null;

                    switch (binaryExp.NodeType)
                    {
                        case ExpressionType.Add: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Add(left, right), typeof(object)); break;
                        case ExpressionType.AddChecked: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.AddChecked(left, right), typeof(object)); break;
                        case ExpressionType.And: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.And(left, right), typeof(object)); break;
                        case ExpressionType.AndAlso: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.AndAlso(left, right), typeof(object)); break;
                        case ExpressionType.ArrayIndex: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.ArrayIndex(left, right), typeof(object)); break;
                        case ExpressionType.Coalesce: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Coalesce(left, right), typeof(object)); break;
                        case ExpressionType.Divide: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Divide(left, right), typeof(object)); break;
                        case ExpressionType.Equal: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Equal(left, right), typeof(object)); break;
                        case ExpressionType.ExclusiveOr: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.ExclusiveOr(left, right), typeof(object)); break;
                        case ExpressionType.GreaterThan: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.GreaterThan(left, right), typeof(object)); break;
                        case ExpressionType.GreaterThanOrEqual: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.GreaterThanOrEqual(left, right), typeof(object)); break;
                        case ExpressionType.LeftShift: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.LeftShift(left, right), typeof(object)); break;
                        case ExpressionType.LessThan: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.LessThan(left, right), typeof(object)); break;
                        case ExpressionType.LessThanOrEqual: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.LessThanOrEqual(left, right), typeof(object)); break;
                        case ExpressionType.Modulo: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Modulo(left, right), typeof(object)); break;
                        case ExpressionType.Multiply: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Multiply(left, right), typeof(object)); break;
                        case ExpressionType.MultiplyChecked: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.MultiplyChecked(left, right), typeof(object)); break;
                        case ExpressionType.NotEqual: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.NotEqual(left, right), typeof(object)); break;
                        case ExpressionType.Or: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Or(left, right), typeof(object)); break;
                        case ExpressionType.OrElse: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.OrElse(left, right), typeof(object)); break;
                        case ExpressionType.Power: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Power(left, right), typeof(object)); break;
                        case ExpressionType.RightShift: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.RightShift(left, right), typeof(object)); break;
                        case ExpressionType.Subtract: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Subtract(left, right), typeof(object)); break;
                        case ExpressionType.SubtractChecked: body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.SubtractChecked(left, right), typeof(object)); break;
                        default:
                            throw new NotSupportedException("I'm sorry. Currently unresponsive at LambdicSql. Please consider another way of writing. And please send us an issue.");
                    }

                    getter = CreateGetter(new[] { binaryExp.Left.Type, binaryExp.Right.Type });
                    getter.Init(body, new[] { left, right }.ToArray());
                    _memberGet[getterName] = getter;
                }
            }

            return getter.GetMemberObject(args.ToArray());
        }
        
        internal static object GetMemberInitObject(MemberInitExpression memberInit)
        {
            var args = new List<object>();
            var paramTypes = new List<Type>();
            var prams = new List<ParameterExpression>();

            //get new expression info.
            var newExp = memberInit.NewExpression;
            var newParameteInfos = newExp.Constructor.GetParameters();
            for (int i = 0; i < newParameteInfos.Length; i++)
            {
                var type = newParameteInfos[i].ParameterType;
                paramTypes.Add(type);
                prams.Add(System.Linq.Expressions.Expression.Parameter(type, "p" + i));
                object arg;
                GetExpressionObject(newExp.Arguments[i], out arg);
                args.Add(arg);
            }

            //add member assignment info.
            int offset = paramTypes.Count;
            var assignments = memberInit.Bindings.Select(e => ((MemberAssignment)e).Expression).ToArray();
            var memberAssignments = new List<MemberAssignment>();
            for (int i = 0; i < assignments.Length; i++)
            {
                paramTypes.Add(assignments[i].Type);
                var p = System.Linq.Expressions.Expression.Parameter(assignments[i].Type, "p" + (offset + i));
                prams.Add(p);
                memberAssignments.Add(System.Linq.Expressions.Expression.Bind(memberInit.Bindings[i].Member, p));
                object arg;
                GetExpressionObject(assignments[i], out arg);
                args.Add(arg);
            }

            //name.
            var getterName = memberInit.NewExpression.Type.FullName +
                "(" + string.Join(",", paramTypes.Take(offset).Select(e => e.FullName).ToArray()) + ")" +
                "()(" + string.Join(",", paramTypes.Skip(offset).Select(e => e.FullName).ToArray()) + ")";

            //getter.
            IGetter getter;
            lock (_memberGet)
            {
                if (!_memberGet.TryGetValue(getterName, out getter))
                {
                    var body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.MemberInit(System.Linq.Expressions.Expression.New(newExp.Constructor, prams.Take(offset).ToArray()), memberAssignments.ToArray()), typeof(object));
                    getter = CreateGetter(paramTypes.ToArray());
                    getter.Init(body, prams.ToArray());
                    _memberGet[getterName] = getter;
                }
            }
            return getter.GetMemberObject(args.ToArray());
        }
        
        internal static object GetNewObject(NewExpression newExp)
        {
            //arguments.
            var ps = newExp.Constructor.GetParameters().Select(e => e.ParameterType).ToList();
            var psExp = new List<ParameterExpression>();
            var args = new List<object>();
            for (int i = 0; i < ps.Count; i++)
            {
                psExp.Add(System.Linq.Expressions.Expression.Parameter(ps[i], "p" + i));
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
                    var body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.New(newExp.Constructor, psExp.ToArray()), typeof(object));
                    getter = CreateGetter(ps.ToArray());
                    getter.Init(body, psExp.ToArray());
                    _memberGet[getterName] = getter;
                }
            }
            return getter.GetMemberObject(args.ToArray());
        }

        static object GetMethodObject(MethodCallExpression method)
        {
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
                psExp.Add(System.Linq.Expressions.Expression.Parameter(ps[i], "p" + i));
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
                    System.Linq.Expressions.Expression body = null;
                    if (instance == null)
                    {
                        body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Call(null, method.Method, psExp.ToArray()), typeof(object));
                    }
                    else
                    {
                        var instanceExp = System.Linq.Expressions.Expression.Parameter(instance.GetType(), "Instance");
                        body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Call(instanceExp, method.Method, psExp.ToArray()), typeof(object));
                        psExp.Add(instanceExp);
                        ps.Add(instance.GetType());
                    }
                    getter = CreateGetter(ps.ToArray());
                    getter.Init(body, psExp.ToArray());
                    _memberGet[getterName] = getter;
                }
            }
            return getter.GetMemberObject(args.ToArray());
        }

        static object GetMemberObject(MemberExpression exp)
        {
            var names = new List<string>();
            object targt = null;
            Type type = null;

            //find member root object.
            var member = exp;
            while (member != null)
            {
                names.Add(member.Member.Name);
              
                //static method.
                if (member.Expression == null)
                {
                    type = member.Member.DeclaringType;
                    break;
                }

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
                    targt = GetMethodObject(method);
                    break;
                }

                var newExp = member.Expression as NewExpression;
                if (newExp != null)
                {
                    type = newExp.Type;
                    targt = GetNewObject(newExp);
                    break;
                }

                member = member.Expression as MemberExpression;
            }
            
            //name.
            var getterName = type.FullName + "@" + string.Join("@", names.ToArray());

            //getter.
            IGetter getter;
            lock (_memberGet)
            {
                if (!_memberGet.TryGetValue(getterName, out getter))
                {
                    var param = System.Linq.Expressions.Expression.Parameter(type, "param");
                    System.Linq.Expressions.Expression target = param;
                    names.Reverse();
                    if (targt == null)
                    {
                        target = StaticPropertyOrField(type, names[0]);
                        names.RemoveAt(0);
                    }
                    names.ForEach(e => target = System.Linq.Expressions.Expression.PropertyOrField(target, e));
                    getter = Activator.CreateInstance(typeof(GetterCore<>).MakeGenericType(type), true) as IGetter;
                    getter.Init(System.Linq.Expressions.Expression.Convert(target, typeof(object)), new[] { param });
                    _memberGet.Add(getterName, getter);
                }
            }
            return getter.GetMemberObject(new object[] { targt });
        }

        internal static object ConvertObject(Type dstType, object src)
        {
            var srcType = src.GetType();

            //getter name.
            var getterName = srcType.FullName + "@-@" + dstType.FullName;

            //getter.
            IGetter getter;
            lock (_memberGet)
            {
                if (!_memberGet.TryGetValue(getterName, out getter))
                {
                    var param = System.Linq.Expressions.Expression.Parameter(srcType, "src");
                    var body = System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Convert(param, dstType), typeof(object));
                    getter = ExpressionToObject.CreateGetter(new Type[] { srcType });
                    getter.Init(body, new[] { param });
                    _memberGet.Add(getterName, getter);
                }
            }
            return getter.GetMemberObject(new[] { src });
        }

        static MemberExpression StaticPropertyOrField(Type type, string propertyOrFieldName)
        {
            var flgs = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            var property = type.GetProperty(propertyOrFieldName, flgs);
            if (property != null) return System.Linq.Expressions.Expression.Property(null, property);

            var field = type.GetField(propertyOrFieldName, flgs);
            if (field != null) return System.Linq.Expressions.Expression.Field(null, field);

            throw new NotSupportedException();
        }
    }
}

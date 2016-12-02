using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;

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
            bool not = false;
            while (true)
            {
                var unary = exp as UnaryExpression;
                if (unary == null)
                {
                    break;
                }
                if (unary.NodeType == ExpressionType.Not)
                {
                    not = !not;
                }
                exp = unary.Operand;
            }
            var ret = GetExpressionObjectCore(exp, out obj);
            if (not)
            {
                obj = !((bool)obj);
            }
            return ret;
        }

        static bool GetExpressionObjectCore(Expression exp, out object obj)
        {
            var binaryExp = exp as BinaryExpression;
            if (binaryExp != null)
            {
                return GetBinaryExpression(binaryExp, out obj);
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

        public static bool GetBinaryExpression(BinaryExpression binaryExp, out object value)
        {
            value = null;

            object objLeft, objRight;
            if (!GetExpressionObject(binaryExp.Left, out objLeft) ||
                !GetExpressionObject(binaryExp.Right, out objRight))
            {
                throw new NotSupportedException();
            }

            //arguments.
            var left = Expression.Parameter(binaryExp.Left.Type, "left");
            var right = Expression.Parameter(binaryExp.Right.Type, "right");
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
                    Expression body = null;

                    switch (binaryExp.NodeType)
                    {
                        case ExpressionType.Add: body = Expression.Convert(Expression.Add(left, right), typeof(object)); break;
                        case ExpressionType.AddChecked: body = Expression.Convert(Expression.AddChecked(left, right), typeof(object)); break;
                        case ExpressionType.And: body = Expression.Convert(Expression.And(left, right), typeof(object)); break;
                        case ExpressionType.AndAlso: body = Expression.Convert(Expression.AndAlso(left, right), typeof(object)); break;
                      //  case ExpressionType.ArrayLength: body = Expression.Convert(Expression.ArrayLength(left, right), typeof(object)); break;
                        case ExpressionType.ArrayIndex: body = Expression.Convert(Expression.ArrayIndex(left, right), typeof(object)); break;
                    //    case ExpressionType.Call: body = Expression.Convert(Expression.Call(left, right), typeof(object)); break;
                        case ExpressionType.Coalesce: body = Expression.Convert(Expression.Coalesce(left, right), typeof(object)); break;
                       // case ExpressionType.Conditional: body = Expression.Convert(Expression.Conditional(left, right), typeof(object)); break;
                    //    case ExpressionType.Constant: body = Expression.Convert(Expression.Constant(left, right), typeof(object)); break;
                     //   case ExpressionType.Convert: body = Expression.Convert(Expression.Convert(left, right), typeof(object)); break;
                      //  case ExpressionType.ConvertChecked: body = Expression.Convert(Expression.ConvertChecked(left, right), typeof(object)); break;
                        case ExpressionType.Divide: body = Expression.Convert(Expression.Divide(left, right), typeof(object)); break;
                        case ExpressionType.Equal: body = Expression.Convert(Expression.Equal(left, right), typeof(object)); break;
                        case ExpressionType.ExclusiveOr: body = Expression.Convert(Expression.ExclusiveOr(left, right), typeof(object)); break;
                        case ExpressionType.GreaterThan: body = Expression.Convert(Expression.GreaterThan(left, right), typeof(object)); break;
                        case ExpressionType.GreaterThanOrEqual: body = Expression.Convert(Expression.GreaterThanOrEqual(left, right), typeof(object)); break;
                     //   case ExpressionType.Invoke: body = Expression.Convert(Expression.Invoke(left, right), typeof(object)); break;
                     //   case ExpressionType.Lambda: body = Expression.Convert(Expression.Lambda(left, right), typeof(object)); break;
                        case ExpressionType.LeftShift: body = Expression.Convert(Expression.LeftShift(left, right), typeof(object)); break;
                        case ExpressionType.LessThan: body = Expression.Convert(Expression.LessThan(left, right), typeof(object)); break;
                        case ExpressionType.LessThanOrEqual: body = Expression.Convert(Expression.LessThanOrEqual(left, right), typeof(object)); break;
                      //  case ExpressionType.ListInit: body = Expression.Convert(Expression.ListInit(left, right), typeof(object)); break;
                      //  case ExpressionType.MemberAccess: body = Expression.Convert(Expression.MemberAccess(left, right), typeof(object)); break;
                      //  case ExpressionType.MemberInit: body = Expression.Convert(Expression.XXX(left, right), typeof(object)); break;
                        case ExpressionType.Modulo: body = Expression.Convert(Expression.Modulo(left, right), typeof(object)); break;
                        case ExpressionType.Multiply: body = Expression.Convert(Expression.Multiply(left, right), typeof(object)); break;
                        case ExpressionType.MultiplyChecked: body = Expression.Convert(Expression.MultiplyChecked(left, right), typeof(object)); break;
                       // case ExpressionType.Negate: body = Expression.Convert(Expression.Negate(left, right), typeof(object)); break;
                       // case ExpressionType.UnaryPlus: body = Expression.Convert(Expression.UnaryPlus(left, right), typeof(object)); break;
                    //    case ExpressionType.NegateChecked: body = Expression.Convert(Expression.NegateChecked(left, right), typeof(object)); break;
                    //    case ExpressionType.New: body = Expression.Convert(Expression.XXX(left, right), typeof(object)); break;
                    //    case ExpressionType.NewArrayInit: body = Expression.Convert(Expression.XXX(left, right), typeof(object)); break;
                    //    case ExpressionType.NewArrayBounds: body = Expression.Convert(Expression.XXX(left, right), typeof(object)); break;
                     //@@@   case ExpressionType.Not: body = Expression.Convert(Expression.Not(left, right), typeof(object)); break;
                        case ExpressionType.NotEqual: body = Expression.Convert(Expression.NotEqual(left, right), typeof(object)); break;
                        case ExpressionType.Or: body = Expression.Convert(Expression.Or(left, right), typeof(object)); break;
                        case ExpressionType.OrElse: body = Expression.Convert(Expression.OrElse(left, right), typeof(object)); break;
                    //    case ExpressionType.Parameter: body = Expression.Convert(Expression.Parameter(left, right), typeof(object)); break;
                        case ExpressionType.Power: body = Expression.Convert(Expression.Power(left, right), typeof(object)); break;
                  //      case ExpressionType.Quote: body = Expression.Convert(Expression.Quote(left, right), typeof(object)); break;
                        case ExpressionType.RightShift: body = Expression.Convert(Expression.RightShift(left, right), typeof(object)); break;
                        case ExpressionType.Subtract: body = Expression.Convert(Expression.Subtract(left, right), typeof(object)); break;
                        case ExpressionType.SubtractChecked: body = Expression.Convert(Expression.SubtractChecked(left, right), typeof(object)); break;
                     //   case ExpressionType.TypeAs: body = Expression.Convert(Expression.TypeAs(left, right), typeof(object)); break;
                     //   case ExpressionType.TypeIs: body = Expression.Convert(Expression.XXX(left, right), typeof(object)); break;
                    }

                    getter = CreateGetter(new[] { binaryExp.Left.Type, binaryExp.Right.Type });
                    getter.Init(body, new[] { left, right }.ToArray());
                    _memberGet[getterName] = getter;
                }
            }
            value = getter.GetMemberObject(args.ToArray());
            return true;
        }

        //TODO refactoring.
        public static bool GetMemberInitObject(MemberInitExpression memberInit, out object value)
        {
            value = null;

            var newExp = memberInit.NewExpression;
            var paramsTypes = newExp.Constructor.GetParameters().Select(e => e.ParameterType).ToList();
            var prams = new List<ParameterExpression>();
            var args = new List<object>();
            for (int i = 0; i < paramsTypes.Count; i++)
            {
                prams.Add(Expression.Parameter(paramsTypes[i], "p" + i));
                object arg;
                GetExpressionObject(newExp.Arguments[i], out arg);
                args.Add(arg);
            }
            
            int offset = paramsTypes.Count;
            var assignments = memberInit.Bindings.Select(e => ((MemberAssignment)e).Expression).ToList();
            paramsTypes.AddRange(assignments.Select(e => e.Type));

            prams.AddRange(assignments.Select((e, i) => Expression.Parameter(e.Type, "p" + (offset + i))));

            var bs = memberInit.Bindings.Select((e, i) => Expression.Bind(e.Member, prams[offset + i])).ToArray();
            for (int i = 0; i < assignments.Count; i++)
            {
                object arg;
                GetExpressionObject(assignments[i], out arg);
                args.Add(arg);
            }

            //name.
            var getterName = memberInit.NewExpression.Type.FullName +
                "(" + string.Join(",", paramsTypes.Take(offset).Select(e => e.FullName).ToArray()) + ")" + 
                "()(" + string.Join(",", paramsTypes.Skip(offset).Select(e => e.FullName).ToArray()) + ")";

            //getter.
            IGetter getter;
            lock (_memberGet)
            {
                if (!_memberGet.TryGetValue(getterName, out getter))
                {
                    Expression body = null;
                    body = Expression.Convert(Expression.MemberInit(Expression.New(newExp.Constructor, prams.Take(offset).ToArray()), bs), typeof(object));
                    getter = CreateGetter(paramsTypes.ToArray());
                    getter.Init(body, prams.ToArray());
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
              
                if (member.Expression == null)
                {
                    //static
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
                    if (!GetMethodObject(method, out targt)) return false;
                }
                var newExp = member.Expression as NewExpression;
                if (newExp != null)
                {
                    type = newExp.Type;
                    if (!GetNewObject(newExp, out targt)) return false;
                }
                member = member.Expression as MemberExpression;
            }
            if (type == null)
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
                if (targt == null)
                {
                    target = StaticPropertyOrField(type, names[0]);
                    names.RemoveAt(0);
                }
                names.ForEach(e => target = Expression.PropertyOrField(target, e));
                getter = Activator.CreateInstance(typeof(GetterCore<>).MakeGenericType(type), true) as IGetter;
                getter.Init(Expression.Convert(target, typeof(object)), new[] { param });
                _memberGet.Add(getterName, getter);
            }
            value = getter.GetMemberObject(new object[] { targt });
            return true;
        }

        public static object ConvertObject(Type dstType, object src)
        {
            var srcType = src.GetType();
            var getterName = srcType.FullName + "@-@" + dstType.FullName;
            IGetter getter;
            lock (_memberGet)
            {
                if (_memberGet.TryGetValue(getterName, out getter))
                {
                    return getter.GetMemberObject(new object[] { src });
                }
                var param = Expression.Parameter(srcType, "src");
                var body = Expression.Convert(Expression.Convert(param, dstType), typeof(object));
                getter = ExpressionToObject.CreateGetter(new Type[] { srcType });
                getter.Init(body, new[] { param });
                _memberGet.Add(getterName, getter);
                return getter.GetMemberObject(new[] { src });
            }
        }

        static MemberExpression StaticPropertyOrField(Type type, string propertyOrFieldName)
        {
            PropertyInfo property = type.GetProperty(propertyOrFieldName, BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Static);
            if (property != null)
            {
                return Expression.Property(null, property);
            }
            FieldInfo field = type.GetField(propertyOrFieldName, BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Static);
            if (field == null)
            {
                property = type.GetProperty(propertyOrFieldName, BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Static);
                if (property != null)
                {
                    return Expression.Property(null, property);
                }
                field = type.GetField(propertyOrFieldName, BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Static);
                if (field == null)
                {
                    throw new ArgumentException(string.Format("{0} NotAMemberOfType {1}", propertyOrFieldName, type));
                }
            }
            return Expression.Field(null, field);
        }
    }
}

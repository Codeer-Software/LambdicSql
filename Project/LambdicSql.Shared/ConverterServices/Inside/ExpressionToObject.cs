﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LambdicSql.MultiplatformCompatibe;

namespace LambdicSql.ConverterServices.Inside
{
    internal static partial class ExpressionToObject
    {
        interface IGetter
        {
            void Init(Expression exp, ParameterExpression[] param);
            object GetMemberObject(object[] arguments);
        }

        static Dictionary<string, IGetter> _memberGet = new Dictionary<string, IGetter>();

        internal static bool GetExpressionObject(Expression exp, out object obj)
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

        static bool GetExpressionObjectCore(Expression exp, out object obj)
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
                        case ExpressionType.ArrayIndex: body = Expression.Convert(Expression.ArrayIndex(left, right), typeof(object)); break;
                        case ExpressionType.Coalesce: body = Expression.Convert(Expression.Coalesce(left, right), typeof(object)); break;
                        case ExpressionType.Divide: body = Expression.Convert(Expression.Divide(left, right), typeof(object)); break;
                        case ExpressionType.Equal: body = Expression.Convert(Expression.Equal(left, right), typeof(object)); break;
                        case ExpressionType.ExclusiveOr: body = Expression.Convert(Expression.ExclusiveOr(left, right), typeof(object)); break;
                        case ExpressionType.GreaterThan: body = Expression.Convert(Expression.GreaterThan(left, right), typeof(object)); break;
                        case ExpressionType.GreaterThanOrEqual: body = Expression.Convert(Expression.GreaterThanOrEqual(left, right), typeof(object)); break;
                        case ExpressionType.LeftShift: body = Expression.Convert(Expression.LeftShift(left, right), typeof(object)); break;
                        case ExpressionType.LessThan: body = Expression.Convert(Expression.LessThan(left, right), typeof(object)); break;
                        case ExpressionType.LessThanOrEqual: body = Expression.Convert(Expression.LessThanOrEqual(left, right), typeof(object)); break;
                        case ExpressionType.Modulo: body = Expression.Convert(Expression.Modulo(left, right), typeof(object)); break;
                        case ExpressionType.Multiply: body = Expression.Convert(Expression.Multiply(left, right), typeof(object)); break;
                        case ExpressionType.MultiplyChecked: body = Expression.Convert(Expression.MultiplyChecked(left, right), typeof(object)); break;
                        case ExpressionType.NotEqual: body = Expression.Convert(Expression.NotEqual(left, right), typeof(object)); break;
                        case ExpressionType.Or: body = Expression.Convert(Expression.Or(left, right), typeof(object)); break;
                        case ExpressionType.OrElse: body = Expression.Convert(Expression.OrElse(left, right), typeof(object)); break;
                        case ExpressionType.Power: body = Expression.Convert(Expression.Power(left, right), typeof(object)); break;
                        case ExpressionType.RightShift: body = Expression.Convert(Expression.RightShift(left, right), typeof(object)); break;
                        case ExpressionType.Subtract: body = Expression.Convert(Expression.Subtract(left, right), typeof(object)); break;
                        case ExpressionType.SubtractChecked: body = Expression.Convert(Expression.SubtractChecked(left, right), typeof(object)); break;
                        default:
                            throw new NotSupportedException("I'm sorry. Currently unresponsive at LambdicSql. Please consider another way of writing. And please send us an issue.");
                    }

                    getter = CreateGetter(new[] { binaryExp.Left.Type, binaryExp.Right.Type });
                    getter.Init(body, new[] { left, right });
                    _memberGet[getterName] = getter;
                }
            }

            return getter.GetMemberObject(args.ToArray());
        }
        
        internal static object GetMemberInitObject(MemberInitExpression memberInit)
        {
            var args = new List<object>();
            var paramTypes = new List<Type>();
            var newArgumentTypeNames = new List<string>();
            var prams = new List<ParameterExpression>();
            var newArgumentParams = new List<ParameterExpression>();

            //get new expression info.
            var newExp = memberInit.NewExpression;
            var newParameteInfos = newExp.Constructor.GetParameters();
            for (int i = 0; i < newParameteInfos.Length; i++)
            {
                var type = newParameteInfos[i].ParameterType;
                paramTypes.Add(type);
                newArgumentTypeNames.Add(type.FullName);
                var param = Expression.Parameter(type, "p" + i);
                prams.Add(param);
                newArgumentParams.Add(param);
                object arg;
                GetExpressionObject(newExp.Arguments[i], out arg);
                args.Add(arg);
            }

            //add member assignment info.
            int offset = paramTypes.Count;
            var assignments = new Expression[memberInit.Bindings.Count];
            for (int i = 0; i < assignments.Length; i++)
            {
                assignments[i] = ((MemberAssignment)memberInit.Bindings[i]).Expression;
            }
            var memberAssignments = new List<MemberAssignment>();
            var assignmentTypeNames = new List<string>();
            for (int i = 0; i < assignments.Length; i++)
            {
                paramTypes.Add(assignments[i].Type);
                assignmentTypeNames.Add(assignments[i].Type.FullName);
                var p = Expression.Parameter(assignments[i].Type, "p" + (offset + i));
                prams.Add(p);
                memberAssignments.Add(Expression.Bind(memberInit.Bindings[i].Member, p));
                object arg;
                GetExpressionObject(assignments[i], out arg);
                args.Add(arg);
            }

            //name.
            var getterName = memberInit.NewExpression.Type.FullName +
                "(" + string.Join(",", newArgumentTypeNames.ToArray()) + ")" +
                "()(" + string.Join(",", assignmentTypeNames.ToArray()) + ")";

            //getter.
            IGetter getter;
            lock (_memberGet)
            {
                if (!_memberGet.TryGetValue(getterName, out getter))
                {
                    var body = Expression.Convert(Expression.MemberInit(Expression.New(newExp.Constructor, newArgumentParams.ToArray()), memberAssignments.ToArray()), typeof(object));
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
            var src = newExp.Constructor.GetParameters();
            var paramterTypes = new Type[src.Length];
            var parameterTypeFullNames = new string[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                paramterTypes[i] = src[i].ParameterType;
                parameterTypeFullNames[i] = paramterTypes[i].FullName;
            }
            
            var psExp = new List<ParameterExpression>();
            var args = new List<object>();
            for (int i = 0; i < paramterTypes.Length; i++)
            {
                psExp.Add(Expression.Parameter(paramterTypes[i], "p" + i));
                object arg;
                GetExpressionObject(newExp.Arguments[i], out arg);
                args.Add(arg);
            }
            
            //name.
            var getterName = newExp.Type.FullName + 
                "(" + string.Join(",", parameterTypeFullNames) + ")";

            //getter.
            IGetter getter;
            lock (_memberGet)
            {
                if (!_memberGet.TryGetValue(getterName, out getter))
                {
                    var body = Expression.Convert(Expression.New(newExp.Constructor, psExp.ToArray()), typeof(object));
                    getter = CreateGetter(paramterTypes);
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
            var src = method.Method.GetParameters();
            var paramterTypes = new Type[src.Length];
            var parameterTypeFullNames = new string[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                paramterTypes[i] = src[i].ParameterType;
                parameterTypeFullNames[i] = paramterTypes[i].FullName;
            }

            var psExp = new List<ParameterExpression>();
            var args = new List<object>();
            for (int i = 0; i < paramterTypes.Length; i++)
            {
                psExp.Add(Expression.Parameter(paramterTypes[i], "p" + i));
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
                "(" + string.Join(",", parameterTypeFullNames) + ")";

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

                        //paramterTypes.Add(instance.GetType());
                        var dstParameterTypes = new Type[paramterTypes.Length + 1];
                        for (int i = 0; i < paramterTypes.Length; i++)
                        {
                            dstParameterTypes[i] = paramterTypes[i];
                        }
                        dstParameterTypes[dstParameterTypes.Length - 1] = instance.GetType();
                        paramterTypes = dstParameterTypes;
                    }
                    getter = CreateGetter(paramterTypes);
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
                    var param = Expression.Parameter(type, "param");
                    Expression target = param;
                    names.Reverse();
                    if (targt == null)
                    {
                        target = ReflectionAdapter.StaticPropertyOrField(type, names[0]);
                        names.RemoveAt(0);
                    }
                    if (names.Count == 0)
                    {
                        getter = ReflectionAdapter.CreateInstance(typeof(GetterCore), true) as IGetter;
                        getter.Init(Expression.Convert(target, typeof(object)), new ParameterExpression[0]);
                    }
                    else
                    {
                        foreach (var e in names)
                        {
                            target = Expression.PropertyOrField(target, e);
                        }
                        getter = ReflectionAdapter.CreateInstance(typeof(GetterCore<>).MakeGenericType(type), true) as IGetter;
                        getter.Init(Expression.Convert(target, typeof(object)), new[] { param });
                    }
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
                    var param = Expression.Parameter(srcType, "src");
                    var body = Expression.Convert(Expression.Convert(param, dstType), typeof(object));
                    getter = ExpressionToObject.CreateGetter(new Type[] { srcType });
                    getter.Init(body, new[] { param });
                    _memberGet.Add(getterName, getter);
                }
            }
            return getter.GetMemberObject(new[] { src });
        }
    }
}

using System;
using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    public static partial class ExpressionToObject
    {
        class GetterCore : IGetter
        {
            Func<object> _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func<object>>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func();
        }
        class GetterCore<T0> : IGetter
        {
            public delegate object Func(T0 t0);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0]);
        }

        class GetterCore<T0, T1> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1]);
        }

        class GetterCore<T0, T1, T2> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2]);
        }

        class GetterCore<T0, T1, T2, T3> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3]);
        }

        class GetterCore<T0, T1, T2, T3, T4> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18], (T19)arguments[19]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, T20 t20);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18], (T19)arguments[19], (T20)arguments[20]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, T20 t20, T21 t21);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18], (T19)arguments[19], (T20)arguments[20], (T21)arguments[21]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, T20 t20, T21 t21, T22 t22);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18], (T19)arguments[19], (T20)arguments[20], (T21)arguments[21], (T22)arguments[22]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, T20 t20, T21 t21, T22 t22, T23 t23);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18], (T19)arguments[19], (T20)arguments[20], (T21)arguments[21], (T22)arguments[22], (T23)arguments[23]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, T20 t20, T21 t21, T22 t22, T23 t23, T24 t24);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18], (T19)arguments[19], (T20)arguments[20], (T21)arguments[21], (T22)arguments[22], (T23)arguments[23], (T24)arguments[24]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, T20 t20, T21 t21, T22 t22, T23 t23, T24 t24, T25 t25);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18], (T19)arguments[19], (T20)arguments[20], (T21)arguments[21], (T22)arguments[22], (T23)arguments[23], (T24)arguments[24], (T25)arguments[25]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, T20 t20, T21 t21, T22 t22, T23 t23, T24 t24, T25 t25, T26 t26);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18], (T19)arguments[19], (T20)arguments[20], (T21)arguments[21], (T22)arguments[22], (T23)arguments[23], (T24)arguments[24], (T25)arguments[25], (T26)arguments[26]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, T20 t20, T21 t21, T22 t22, T23 t23, T24 t24, T25 t25, T26 t26, T27 t27);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18], (T19)arguments[19], (T20)arguments[20], (T21)arguments[21], (T22)arguments[22], (T23)arguments[23], (T24)arguments[24], (T25)arguments[25], (T26)arguments[26], (T27)arguments[27]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, T20 t20, T21 t21, T22 t22, T23 t23, T24 t24, T25 t25, T26 t26, T27 t27, T28 t28);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18], (T19)arguments[19], (T20)arguments[20], (T21)arguments[21], (T22)arguments[22], (T23)arguments[23], (T24)arguments[24], (T25)arguments[25], (T26)arguments[26], (T27)arguments[27], (T28)arguments[28]);
        }

        class GetterCore<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29> : IGetter
        {
            public delegate object Func(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, T20 t20, T21 t21, T22 t22, T23 t23, T24 t24, T25 t25, T26 t26, T27 t27, T28 t28, T29 t29);
            Func _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T0)arguments[0], (T1)arguments[1], (T2)arguments[2], (T3)arguments[3], (T4)arguments[4], (T5)arguments[5], (T6)arguments[6], (T7)arguments[7], (T8)arguments[8], (T9)arguments[9], (T10)arguments[10], (T11)arguments[11], (T12)arguments[12], (T13)arguments[13], (T14)arguments[14], (T15)arguments[15], (T16)arguments[16], (T17)arguments[17], (T18)arguments[18], (T19)arguments[19], (T20)arguments[20], (T21)arguments[21], (T22)arguments[22], (T23)arguments[23], (T24)arguments[24], (T25)arguments[25], (T26)arguments[26], (T27)arguments[27], (T28)arguments[28], (T29)arguments[29]);
        }
    }
}

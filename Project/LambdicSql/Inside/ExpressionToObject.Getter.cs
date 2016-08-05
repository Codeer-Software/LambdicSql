using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    static partial class ExpressionToObject
    {
        class GetterCore : IGetter
        {
            Func<object> _func;
            public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func<object>>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func();
        }
        class GetterCore<T> : IGetter
        {
            Func<T, object> _func;
            public void Init(Expression exp, ParameterExpression[] param)=> _func = Expression.Lambda<Func<T, object>>(exp, param).Compile();
            public object GetMemberObject(object[] arguments) => _func((T)arguments[0]);
        }
    }
}

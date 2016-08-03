﻿using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    public interface ISqlExpression
    {
        DbInfo DbInfo { get; }
        string ToString(ISqlStringConverter decoder);
        Expression Expression { get; }
    }

    public interface ISqlExpression<TSelected> : ISqlExpression 
        { }
    
    public interface ISqlExpression<TDB, TSelected> : ISqlExpression<TSelected>
    { }
}

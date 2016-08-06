﻿using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    class SqlExpressionSingle<TSelected> : SqlExpression<TSelected>
    {
        Expression _core;
        public override DbInfo DbInfo { get; protected set; }

        public SqlExpressionSingle(DbInfo dbInfo, Expression core)
        {
            DbInfo = dbInfo;
            _core = core;
        }
        
        public override string ToString(ISqlStringConverter converter)
            => (_core == null) ? string.Empty : converter.ToString(_core);
    }
}
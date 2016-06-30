﻿using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql
{
    public interface IExpressionDecoder
    {
        DbInfo DbInfo { get; }
        string ToString(Expression exp);
        string ToStringObject(object obj);
        string MakeSqlArguments(IEnumerable<object> v);
    }
}

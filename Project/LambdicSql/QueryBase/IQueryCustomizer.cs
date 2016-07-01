using System;

namespace LambdicSql.QueryBase
{
    public interface IQueryCustomizer
    {
        string CustomOperator(Type type1, string @operator, Type type2);
    }
}

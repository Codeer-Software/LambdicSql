using System;

namespace LambdicSql.QueryBase
{
    public interface IQueryCustomizer
    {
        string CustomOperator(Type type1, string @operator, Type type2);
        string CusotmInvoke(Type returnType, string name, DecodedInfo[] argSrc);
    }
}

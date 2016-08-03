using LambdicSql.QueryBase;
using System;

namespace LambdicSql.PostgreSql
{
    public class PostgreSqlCustomizer : IQueryCustomizer
    {
        public string CustomOperator(Type type1, string @operator, Type type2)
        {
            if ((type1 == typeof(string) || type2 == typeof(string)) && @operator == "+")
            {
                return "||";
            }
            return @operator;
        }
        public string CusotmInvoke(Type returnType, string name, DecodedInfo[] argSrc) => null;
    }
}

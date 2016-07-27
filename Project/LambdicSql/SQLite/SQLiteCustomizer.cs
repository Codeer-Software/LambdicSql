using LambdicSql.QueryBase;
using System;

namespace LambdicSql.SQLite
{
    public class SQLiteCustomizer : IQueryCustomizer
    {
        public string CustomOperator(Type type1, string @operator, Type type2)
        {
            if ((type1 == typeof(string) || type2 == typeof(string)) && @operator == "+")
            {
                return "||";
            }
            return @operator;
        }
        public IClause[] CustomClauses(IClause[] clauses) => clauses;
        public string CusotmInvoke(CustomTargetType invokeType, Type returnType, string name, DecodedInfo[] argSrc) => null;
    }
}

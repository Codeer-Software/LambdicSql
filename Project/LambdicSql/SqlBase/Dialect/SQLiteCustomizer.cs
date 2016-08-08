using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Dialect
{
    public class SQLiteCustomizer : ISqlStringConverterCustomizer
    {
        public string CustomOperator(Type type1, string @operator, Type type2)
        {
            if ((type1 == typeof(string) || type2 == typeof(string)) && @operator == "+")
            {
                return "||";
            }
            return @operator;
        }
        public string CusotmMethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods) => null;
    }
}

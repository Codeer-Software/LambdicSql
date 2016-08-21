using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    public class SqlConvertOption
    {
        public string StringAddOperator { get; set; } = "+";
        public string ParameterPrefix { get; set; } = "@";
        public ICustomSqlSyntax CustomSqlSyntax { get; set; }
    }
}

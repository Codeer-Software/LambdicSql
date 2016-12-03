using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System.Linq.Expressions;

namespace LambdicSql
{
    [SqlSyntax]
    public class SysibmType
    {
        public object Sysdummy1 => InvalitContext.Throw<long>(nameof(Sysdummy1));
        static string ToString(ISqlStringConverter converter, MemberExpression member)
            => "SYSIBM." + member.Member.Name.ToUpper();
    }
}

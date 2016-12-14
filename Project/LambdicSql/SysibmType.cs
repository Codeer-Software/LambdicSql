using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// SYSIBM keyword.
    /// </summary>
    [SqlSyntax]
    public class SysibmType
    {
        internal SysibmType() { }

        /// <summary>
        /// SYSDUMMY1 keyword.
        /// </summary>
        public object Sysdummy1 => InvalitContext.Throw<long>(nameof(Sysdummy1));

        static TextParts ToString(ISqlStringConverter converter, MemberExpression member)
            => new SingleText("SYSIBM." + member.Member.Name.ToUpper());
    }
}

using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    [SqlSyntax]
    public class Asterisk
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Asterisk() { InvalitContext.Throw("new " + nameof(Asterisk)); }

        static SqlText Convert(ISqlStringConverter converter, NewExpression exp) => "*";

        static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods) => "*";
    }

    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    /// <typeparam name="T">It represents the type to select when used in the Select clause.</typeparam>
    [SqlSyntax]
    public class Asterisk<T> : Asterisk
    {
        static SqlText Convert(ISqlStringConverter converter, NewExpression exp) => "*";

        static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods) => "*";
    }
}

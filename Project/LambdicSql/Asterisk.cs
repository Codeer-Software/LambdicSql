using LambdicSql.Inside;
using LambdicSql.SqlBase;
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

        static IText ToString(ISqlStringConverter converter, NewExpression exp)
            => new SingleText("*");

        static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
            => new SingleText("*");
    }

    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    /// <typeparam name="T">It represents the type to select when used in the Select clause.</typeparam>
    [SqlSyntax]
    public class Asterisk<T> : Asterisk { }
}

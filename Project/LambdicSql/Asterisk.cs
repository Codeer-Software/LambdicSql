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
    public interface IAsterisk { }

    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    /// <typeparam name="T">It represents the type to select when used in the Select clause.</typeparam>
    public interface IAsterisk<T> : IAsterisk { }

    /*
    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    [SqlSyntax]
    public class IAsterisk
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public IAsterisk() { InvalitContext.Throw("new " + nameof(IAsterisk)); }

        static SqlText Convert(ISqlStringConverter converter, NewExpression exp) => "*";

        static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods) => "*";
    }

    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    /// <typeparam name="T">It represents the type to select when used in the Select clause.</typeparam>
    [SqlSyntax]
    public class IAsterisk<T> : IAsterisk
    {
        static SqlText Convert(ISqlStringConverter converter, NewExpression exp) => "*";

        static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods) => "*";
    }*/
}

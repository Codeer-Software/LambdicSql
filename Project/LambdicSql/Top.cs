using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// TOP keyword.
    /// </summary>
    [SqlSyntax]
    public class Top
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="count">cout.</param>
        public Top(long count) { InvalitContext.Throw("new " + nameof(Assign)); }

        static SqlText Convert(ISqlStringConverter converter, NewExpression exp)
            => Convert(converter, exp.Arguments);

        static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
            => Convert(converter, methods[0].Arguments);

        static SqlText Convert(ISqlStringConverter converter, ReadOnlyCollection<Expression> arguments)
        {
            var args = arguments.Select(e => converter.Convert(e)).ToArray();
            return "TOP " + converter.Context.Parameters.ResolvePrepare(args[0].ToString(false, 0));
        }
    }
}

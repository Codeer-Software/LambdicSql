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

        static IText ToString(ISqlStringConverter converter, NewExpression exp)
            => ToString(converter, exp.Arguments);

        static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
            => ToString(converter, methods[0].Arguments);

        static IText ToString(ISqlStringConverter converter, ReadOnlyCollection<Expression> arguments)
        {
            //TODO これだめだ。内部的に何度もパラメータ変換が実行される ToString(0)
            var args = arguments.Select(e => converter.ToString(e)).ToArray();
            return new SingleText("TOP " + converter.Context.Parameters.ResolvePrepare(args[0].ToString(0)));
        }
    }
}

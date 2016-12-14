using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// ROWS keyword.
    /// Use it with the OVER function.
    /// </summary>
    [SqlSyntax]
    public class Rows
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        public Rows(int preceding) { InvalitContext.Throw("new " + nameof(Rows)); }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        /// <param name="following">Following row count.</param>
        public Rows(int preceding, int following) { InvalitContext.Throw("new " + nameof(Rows)); }

        static TextParts ToString(ISqlStringConverter converter, NewExpression exp)
        {
            var args = exp.Arguments.Select(e => converter.ToString(e)).ToArray();
            if (exp.Arguments.Count == 1)
            {
                return new SingleText("ROWS " + converter.Context.Parameters.ResolvePrepare(args[0].ToString(0)) + " PRECEDING");
            }
            else
            {
                //TODO これだめだ。内部的に何度もパラメータ変換が実行される ToString(0)
                //Sql server can't use parameter.
                return new SingleText("ROWS BETWEEN " + converter.Context.Parameters.ResolvePrepare(args[0].ToString(0)) +
                    " PRECEDING AND " + converter.Context.Parameters.ResolvePrepare(args[1].ToString(0)) + " FOLLOWING");
            }
        }
    }
}

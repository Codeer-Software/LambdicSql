﻿using LambdicSql.Inside;
using LambdicSql.SqlBase;
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

        static string ToString(ISqlStringConverter converter, NewExpression exp)
        {
            var args = exp.Arguments.Select(e => converter.ToString(e)).ToArray();
            return "TOP " + converter.Context.Parameters.ResolvePrepare(args[0]);
        }

        static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var exp = methods[0];
            var args = exp.Arguments.Select(e => converter.ToString(e)).ToArray();
            return "TOP " + converter.Context.Parameters.ResolvePrepare(args[0]);
        }
    }
}
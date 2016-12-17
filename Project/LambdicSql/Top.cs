using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

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
            return LineSpace("TOP", args[0].Customize(new CustomizeParameterToObject()));
        }
    }

    class CustomizeParameterToObject : ISqlTextCustomizer
    {
        public SqlText Custom(SqlText src)
        {
            var col = src as ParameterText;
            if (col == null) return src;
            return col.ToDisplayValue();
        }
    }
}

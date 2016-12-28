using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class InsertIntoClause
    {
        internal static ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression[] methods)
            => new VText(methods.Select(m => MethodToString(converter, m)).ToArray());

        static ExpressionElement MethodToString(IExpressionConverter converter, MethodCallExpression method)
        {
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.InsertInto): return MethodToStringInsertInto(converter, method);
                case nameof(LambdicSql.Keywords.Values): return MethodToStringValues(converter, method);
            }
            throw new NotSupportedException();
        }

        static ExpressionElement MethodToStringValues(IExpressionConverter converter, MethodCallExpression method)
        {
            var values = Func("VALUES", converter.Convert(method.Arguments[1]));
            values.Indent = 1;
            return values;
        }

        static ExpressionElement MethodToStringInsertInto(IExpressionConverter converter, MethodCallExpression method)
        {
            var table = converter.Convert(method.Arguments[0]);

            //column should not have a table name.
            var arg = converter.Convert(method.Arguments[1]).Customize(new CustomizeColumnOnly());
            return Func(LineSpace("INSERT INTO", table), arg);
        }
    }
}

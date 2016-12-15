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
        internal static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
            => new VText(methods.Select(m => MethodToString(converter, m)).ToArray());

        static SqlText MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.InsertInto): return MethodToStringInsertInto(converter, method);
                case nameof(LambdicSql.Keywords.Values): return MethodToStringValues(converter, method);
            }
            throw new NotSupportedException();
        }

        static SqlText MethodToStringValues(ISqlStringConverter converter, MethodCallExpression method)
        {
            var values = Func("VALUES", converter.Convert(method.Arguments[1]));
            values.Indent = 1;
            return values;
        }

        static SqlText MethodToStringInsertInto(ISqlStringConverter converter, MethodCallExpression method)
        {
            var table = converter.Convert(method.Arguments[0]);

            //column should not have a table name.
            bool src = converter.UsingColumnNameOnly;
            try
            {
                converter.UsingColumnNameOnly = true;
                var arg = converter.Convert(method.Arguments[1]);
                return Func(LineSpace("INSERT INTO", table), arg);
            }
            finally
            {
                converter.UsingColumnNameOnly = src;
            }
        }
    }
}

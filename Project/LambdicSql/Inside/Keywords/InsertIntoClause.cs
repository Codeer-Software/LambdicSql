using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class InsertIntoClause
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
            => string.Join(string.Empty, methods.Select(m => MethodToString(converter, m)).ToArray());

        static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.InsertInto): return MethodToStringInsertInto(converter, method);
                case nameof(LambdicSql.Keywords.Values): return MethodToStringValues(converter, method);
            }
            throw new NotSupportedException();
        }

        static string MethodToStringValues(ISqlStringConverter converter, MethodCallExpression method)
            => Environment.NewLine + "\tVALUES (" + converter.ToString(method.Arguments[1]) + ")";

        static string MethodToStringInsertInto(ISqlStringConverter converter, MethodCallExpression method)
        {
            var table = converter.ToString(method.Arguments[0]);
            //column should not have a table name.
            var arg = converter.ToString(method.Arguments[1]).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => GetColumnOnly(e)).ToArray();
            return Environment.NewLine + "INSERT INTO " + table + "(" + string.Join(", ", arg) + ")";
        }

        static string GetColumnOnly(string src)
        {
            var index = src.LastIndexOf(".");
            return index == -1 ? src : src.Substring(index + 1);
        }
    }
}

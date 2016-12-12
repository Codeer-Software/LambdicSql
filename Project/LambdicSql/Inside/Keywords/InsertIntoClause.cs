using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class InsertIntoClause
    {
        internal static IText ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
            => new VerticalText(methods.Select(m => MethodToString(converter, m)).ToArray());

        static IText MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.InsertInto): return MethodToStringInsertInto(converter, method);
                case nameof(LambdicSql.Keywords.Values): return MethodToStringValues(converter, method);
            }
            throw new NotSupportedException();
        }

        static IText MethodToStringValues(ISqlStringConverter converter, MethodCallExpression method)
            => new HorizontalText() { IsFunctional = true, Indent = 1} + "VALUES (" + converter.ToString(method.Arguments[1]) + ")";

        static IText MethodToStringInsertInto(ISqlStringConverter converter, MethodCallExpression method)
        {
            var table = converter.ToString(method.Arguments[0]);
            //column should not have a table name.
            //TODO ここでargをコンバートしている区間はカラム名称のみにするとかできたらいい。
            bool src = converter.UsingColumnNameOnly;
            try
            {
                converter.UsingColumnNameOnly = true;
                var arg = converter.ToString(method.Arguments[1]);//@@@.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => GetColumnOnly(e)).ToArray();
                return new HorizontalText() { IsFunctional = true } + "INSERT INTO " + table + "(" + new HorizontalText(", ", arg) + ")";
            }
            finally
            {
                converter.UsingColumnNameOnly = src;
            }
        }

        static string GetColumnOnly(string src)
        {
            var index = src.LastIndexOf(".");
            return index == -1 ? src : src.Substring(index + 1);
        }
    }
}

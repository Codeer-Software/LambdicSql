using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq;
using System.Linq.Expressions;

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
            => new HText("VALUES (", converter.Convert(method.Arguments[1]), ")") { IsFunctional = true, Indent = 1};

        static SqlText MethodToStringInsertInto(ISqlStringConverter converter, MethodCallExpression method)
        {
            var table = converter.Convert(method.Arguments[0]);
            //column should not have a table name.
            //TODO ここでargをコンバートしている区間はカラム名称のみにするとかできたらいい。
            bool src = converter.UsingColumnNameOnly;
            try
            {
                converter.UsingColumnNameOnly = true;
                var arg = converter.Convert(method.Arguments[1]);//@@@.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => GetColumnOnly(e)).ToArray();
                return new HText("INSERT INTO ", table, "(", new HText(arg) { Separator = ", " }, ")") { IsFunctional = true };
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

using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace LambdicSql.Inside.Keywords
{
    static class CaseClause
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            foreach (var m in methods)
            {
                var argSrc = m.Arguments.Skip(m.AdjustSqlSyntaxMethodArgumentIndex(0)).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(m.Method.Name, argSrc));
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(string name, string[] argSrc)
        {
            switch (name)
            {
                case nameof(LambdicSql.Keywords.Case):
                    {
                        var text = Environment.NewLine + "\tCASE";
                        if (argSrc.Length == 1)
                        {
                            text += (" " + argSrc[0]);
                        }
                        return text;
                    }
                case nameof(LambdicSql.Keywords.When): return Environment.NewLine + "\t\tWHEN " + AdjustSubQueryString(argSrc[0], "\t\t");
                case nameof(LambdicSql.Keywords.Then): return " THEN " + argSrc[0];
                case nameof(LambdicSql.Keywords.Else): return Environment.NewLine + "\t\tELSE " + argSrc[0];
                case nameof(LambdicSql.Keywords.End): return Environment.NewLine + "\tEND";
            }
            return null;
        }

        //TODO これ色んな所にいるんじゃなかろうか？まあ、サブクエリが入りうるか所なんだけど、統一的に処理できないかな？
        internal static string AdjustSubQueryString(string text, string adjust)
        {
            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Where(e=>!string.IsNullOrEmpty(e.Trim())).ToArray();
            if (lines.Length <= 1) return text;
            lines[0] = InsertTextToTop(lines[0], "(");

            //TODO ここで改行をいきなり先頭に入れるんじゃなくって
            //先頭にSelectがなかったら改行はいらんとか
            if (lines[0].IndexOf("SELECT") == -1)
            {
                return lines[0].Trim() + Environment.NewLine +
                    string.Join(Environment.NewLine, lines.Skip(1).Select(e => adjust + e).ToArray()) + ")";
            }

            return Environment.NewLine + string.Join(Environment.NewLine, lines.Select(e => adjust + e).ToArray()) + ")";
        }
        static string InsertTextToTop(string line, string insertText)
        {
            //なんか微妙
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != '\t')
                {
                    return line.Substring(0, i) + insertText + line.Substring(i);
                }
            }
            return insertText + line;
        }
    }
}

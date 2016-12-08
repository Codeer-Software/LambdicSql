using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LambdicSql.Inside
{
    //TODO
    class SqlDisplayAdjuster
    {
        internal static string Adjust(string text)
        {
            //adjust. remove empty line.
            //ここはこだわりの世界やけど、括弧を前詰めにするか？
            var list = new List<string>();
            var insert = string.Empty;
            foreach (var l in text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var check = l.Trim();
                if (string.IsNullOrEmpty(check)) continue;
                check = insert + check;
                if (string.IsNullOrEmpty(check.Replace("(", string.Empty).Trim()))
                {
                    insert = check.Replace(" ", string.Empty).Replace("\t", string.Empty);
                    continue;
                }
                list.Add(InsertTextToTop(l, insert));
                insert = string.Empty;
            }

            text = string.Join(Environment.NewLine, list.ToArray());
            return text;
        }

        internal static string AdjustSubQuery(Expression e, string v)
        {
            if (typeof(IClauseChain).IsAssignableFrom(e.Type))
            {
                return SqlStringConverter.AdjustSubQueryString(v);
            }
            return v;
        }

        //TODO これ色んな所にいるんじゃなかろうか？まあ、サブクエリが入りうるか所なんだけど、統一的に処理できないかな？
        internal static string AdjustSubQueryString(string text, string adjust)
        {
            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Where(e => !string.IsNullOrEmpty(e.Trim())).ToArray();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LambdicSql.Inside
{
    class SqlDisplayAdjuster
    {

        //TODO
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

using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LambdicSql.Inside
{
    //TODO 表示用の調整
    class SqlDisplayAdjuster
    {
        internal static TextParts AdjustSubQueryString(TextParts text)
        {
            if (text.ToString(0).Replace(Environment.NewLine, string.Empty).Replace("\t", " ").Replace("(", string.Empty).Trim().IndexOf("SELECT") != 0) return text;

            return text.ConcatAround("(", ")");
        }

        internal static TextParts AdjustSubQuery(Expression e, TextParts v)
        {
            if (typeof(IClauseChain).IsAssignableFrom(e.Type))
            {
                return AdjustSubQueryString(v);
            }
            return v;
        }
    }
}

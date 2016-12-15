using LambdicSql.SqlBase;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace LambdicSql.Inside.Keywords
{
    static class ConditionKeyWords
    {
        //TODO サブクエリに関して改善
        //括弧も特別クラスを付ければよかろう？
        //TODO そんでこれは関数を分ける！
        internal static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.Like):
                    {
                        var header = new HText(args[0], " LIKE") { EnableChangeLine = false };
                        return new HText(header, args[1]) { Separator = " ", IsFunctional = true };
                    }
                case nameof(LambdicSql.Keywords.Between):
                    {
                        var header = new HText(args[0], " BETWEEN") { EnableChangeLine = false };
                        return new HText(header, args[1], "AND", args[2]) { Separator = " ", IsFunctional = true };
                    }
                case nameof(LambdicSql.Keywords.In):
                    {
                        var header = new HText(args[0], " IN") { EnableChangeLine = false };
                        return new HText(header, args[1].ConcatAround("(", ")")) { IsFunctional = true };
                    }
                case nameof(LambdicSql.Keywords.Exists):
                    return new HText("EXISTS", args[0]) { Separator = " ", IsFunctional = true };
            }
            return null;
        }
    }
}

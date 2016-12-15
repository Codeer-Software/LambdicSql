using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class UpdateClause
    {
        internal static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<SqlText>();
            SqlText tableName = null;
            foreach (var m in methods)
            {
                list.Add(MethodToString(converter, m, ref tableName));
            }
            return new VText(list.ToArray());
        }

        static SqlText MethodToString(ISqlStringConverter converter, MethodCallExpression method, ref SqlText tableName)
        {
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.Update):
                    {
                        tableName = converter.Convert(method.Arguments[0]);
                        return new HText("UPDATE", tableName) { Separator = " "};
                    }
                case nameof(LambdicSql.Keywords.Set):
                    {
                        //TODO これよくないAssignを継承したクラスを作ってって感じ　それか、Assignはそういうものってするか？
                        var name = tableName + ".";
                        var array = method.Arguments[1] as NewArrayExpression;
                        var texts = new VText();
                        texts.Add("SET");
                        texts.Add(new VText(array.Expressions.Select(e => converter.Convert(e)).ToArray()) { Indent = 1, Separator = "," });
                        return texts;
                   //     return Environment.NewLine + "SET" + Environment.NewLine + "\t" +
                   //         string.Join("," + Environment.NewLine + "\t", array.Expressions.Select(e => converter.ToString(e).Replace(name, string.Empty)).ToArray());
                    }
            }
            throw new NotSupportedException();
        }
    }
}

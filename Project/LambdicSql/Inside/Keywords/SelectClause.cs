using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    //SELECTに関しても AS とこの部分はFunctionalではないようにする
    static class SelectClause
    {
        internal static TextParts ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];

            //ALL, DISTINCT
            var modify = new List<Expression>();
            for (int i = method.SkipMethodChain(0); i < method.Arguments.Count - 1; i++)
            {
                modify.Add(method.Arguments[i]);
            }
            //TODO modifyの扱いが良くないのと、これは改行入れて欲しくないの表現が難しい　後文字列化も良くないけど、しょうがないか？
            //まあ、これは改行入れて欲しくないは文字列でいい気もするけど・・・
            var x = "SELECT";
            if (modify.Count != 0)
            {
                //TODO これだめだ。内部的に何度もパラメータ変換が実行される ToString(0)
                x += " ";
                x += string.Join(" ", modify.Select(e => converter.ToString(e).ToString(0)).ToArray());
            }

            //select core.
            var selectTarget = method.Arguments[method.Arguments.Count - 1];
            TextParts selectTargetText = null;
            ObjectCreateInfo createInfo = null;

            //*
            if (typeof(Asterisk).IsAssignableFrom(selectTarget.Type))
            {
                var asteriskType = selectTarget.Type.IsGenericType ? selectTarget.Type.GetGenericTypeDefinition() : null;
                if (asteriskType == typeof(Asterisk<>)) createInfo = ObjectCreateAnalyzer.MakeSelectInfo(asteriskType);
                x += " *";
            }
            //new { item = db.tbl.column }
            else
            {
                createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTarget);
                selectTargetText =
                    new VText(",", createInfo.Members.Select(e => ToStringSelectedElement(converter, e)).ToArray()) { Indent = 1 };
            }

            //remember creat info.
            if (converter.Context.ObjectCreateInfo == null) converter.Context.ObjectCreateInfo = createInfo;

       //     return Environment.NewLine + string.Join(" ", new[] { "SELECT" }.Concat(modify.Select(e => converter.ToString(e))).ToArray()) + selectTargetText;

           //return select clause text.
            var select = new HText() { Separator = " " } + x;
       //     select.AddRange(modify.Select(e => converter.ToString(e)));
            return selectTargetText == null ? (TextParts)new SingleText(x) : new VText(new SingleText(x), selectTargetText);
        }

        static TextParts ToStringSelectedElement(ISqlStringConverter converter, ObjectCreateMemberInfo element)
        {
            //single select.
            //for example, COUNT(*).
            if (string.IsNullOrEmpty(element.Name)) return converter.ToString(element.Expression);

            //TODO この無理やりくっつけるのはなくてもいいかな
            //normal select.
            return new HText(converter.ToString(element.Expression), " AS ", element.Name) { IsNotLineChange = true };
        }
    }
}

using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class SelectClause
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];

            var define = method.Arguments[method.Arguments.Count - 1];
            var modify = new List<Expression>();
            for (int i = method.AdjustSqlSyntaxMethodArgumentIndex(0); i < method.Arguments.Count - 1; i++)
            {
                modify.Add(method.Arguments[i]);
            }
            var selectText = string.Join(" ", new[] { "SELECT" }.Concat(modify.Select(e => converter.ToString(e))).ToArray());
            
            //TODO refactoring. other imlements.
            if (typeof(Asterisk).IsAssignableFrom(define.Type))
            {
                var asteriskType = define.Type.IsGenericType ? define.Type.GetGenericTypeDefinition() : null;
                if (asteriskType == typeof(Asterisk<>))
                {
                    var select = ObjectCreateAnalyzer.MakeSelectInfo(asteriskType);
                    if (converter.Context.ObjectCreateInfo == null)
                    {
                        converter.Context.ObjectCreateInfo = select;
                    }
                }
                return Environment.NewLine + selectText + " *";
            }
            else
            {
                var select = ObjectCreateAnalyzer.MakeSelectInfo(define);
                if (converter.Context.ObjectCreateInfo == null)
                {
                    converter.Context.ObjectCreateInfo = select;
                }
                return Environment.NewLine + selectText + Environment.NewLine + "\t" +
                    string.Join("," + Environment.NewLine + "\t", select.Members.Select(e => ToString(converter, e)).ToArray());
            }
        }

        static string ToString(ISqlStringConverter decoder, ObjectCreateMemberInfo element)
        {
            if (element.Expression == null)
            {
                return element.Name;
            }
            if (string.IsNullOrEmpty(element.Name))
            {
                return decoder.ToString(element.Expression);
            }
            return decoder.ToString(element.Expression) + " AS " + element.Name;
        }
    }
}

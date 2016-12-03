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

            while (true)
            {
                if (!typeof(ISqlExpressionBase).IsAssignableFrom(define.Type))
                {
                    break;
                }
                object obj;
                ExpressionToObject.GetExpressionObject(define, out obj);
                var exp = ((ISqlExpressionBase)obj).GetExpressions();
                if (exp.Length != 1) throw new NotSupportedException();
                define = exp[0];
            }
                //TODO refactoring. other imlements.
            if (typeof(Asterisk).IsAssignableFrom(define.Type))
            {
                var asteriskType = define.Type.IsGenericType ? define.Type.GetGenericTypeDefinition() : null;
                if (asteriskType == typeof(Asterisk<>))
                {
                    var select = ObjectCreateAnalyzer.MakeSelectInfo(asteriskType);
                    if (converter.Context.SelectClauseInfo == null)
                    {
                        converter.Context.SelectClauseInfo = select;
                    }
                }
                return Environment.NewLine + selectText + " *";
            }
            else
            {
                var select = ObjectCreateAnalyzer.MakeSelectInfo(define);
                if (converter.Context.SelectClauseInfo == null)
                {
                    converter.Context.SelectClauseInfo = select;
                }
                return Environment.NewLine + selectText + Environment.NewLine + "\t" +
                    string.Join("," + Environment.NewLine + "\t", select.Elements.Select(e => ToString(converter, e)).ToArray());
            }
        }
        
        static string ToString(ISqlStringConverter decoder, ObjectCreateMemberElement element)
            => element.Expression == null ? element.Name : decoder.ToString(element.Expression) + " AS " + element.Name;
    }
}

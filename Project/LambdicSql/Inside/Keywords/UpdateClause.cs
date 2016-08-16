using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.Inside.Keywords
{
    static class UpdateClause
    {
        internal static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var tbl = converter.ToString(method.Arguments[0]);
            var select = ObjectCreateAnalyzer.MakeSelectInfo(method.Arguments[1]);
            var list = new List<string>();
            
            var isAll = select.Elements.Any(e => e.Expression == null);
            if (isAll)
            {
                object obj = null;
                ExpressionToObject.GetExpressionObject(method.Arguments[1], out obj);
                var type = method.Arguments[1].Type;
                foreach (var e in select.Elements)
                {
                    var val = type.GetPropertyValue(e.Name, obj);
                    list.Add(Environment.NewLine + "\t" + e.Name + " = " + converter.ToString(val));
                }
            }
            else
            {
                foreach (var e in select.Elements)
                {
                    list.Add(Environment.NewLine + "\t" + e.Name + " = " + converter.ToString(e.Expression));
                }
            }
            return Environment.NewLine + "UPDATE " + tbl +
                    Environment.NewLine + "SET" + string.Join(",", list.ToArray());
        }
    }
}

using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class InsertIntoClause
    {
        internal static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var tbl = converter.ToString(method.Arguments[0]);
            var select = ObjectCreateAnalyzer.MakeSelectInfo(method.Arguments[1]);
            var cols = new List<string>();
            var values = new List<string>();

            object obj = null;
            ExpressionToObject.GetExpressionObject(method.Arguments[1], out obj);
            var type = method.Arguments[1].Type;

            foreach (var e in select.Elements)
            {
                var val = type.GetPropertyValue(e.Name, obj);
                values.Add(converter.ToString(val));
                cols.Add(e.Name);
            }
            return Environment.NewLine + "INSERT INTO " + tbl + "(" + string.Join(",", cols.ToArray()) + ")" + 
                   Environment.NewLine +"VALUES(" + string.Join(",", values.ToArray()) + ")";
        }
    }
}

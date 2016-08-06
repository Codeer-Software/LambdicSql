using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class UpdateClause
    {
        internal static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            foreach (var m in methods)
            {
                list.Add(MethodToString(converter, m));
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.Update): return Environment.NewLine + "UPDATE " + converter.ToString(method.Arguments[0]);
                case nameof(LambdicSql.Keywords.Set):
                    {
                        var select = ObjectCreateAnalyzer.MakeSelectInfo(method.Arguments[1]);
                        var list = new List<string>();
                        foreach (var e in select.Elements)
                        {
                            list.Add(Environment.NewLine + "\t" + e.Name + " = " + converter.ToString(e.Expression)); 
                        }
                        return Environment.NewLine + "SET" + string.Join(",", list.ToArray());
                    }
            }
            throw new NotSupportedException();
        }
    }
}

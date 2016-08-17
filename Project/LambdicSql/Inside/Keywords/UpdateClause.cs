using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class UpdateClause
    {
        internal static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            string tableName = string.Empty;
            foreach (var m in methods)
            {
                list.Add(MethodToString(converter, m, ref tableName));
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(ISqlStringConverter converter, MethodCallExpression method, ref string tableName)
        {
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.Update):
                    {
                        tableName = converter.ToString(method.Arguments[0]);
                        return Environment.NewLine + "UPDATE " + tableName;
                    }
                case nameof(LambdicSql.Keywords.Set):
                    {
                        var name = tableName + ".";
                        var array = method.Arguments[1] as NewArrayExpression;
                        return Environment.NewLine + "SET" + Environment.NewLine + "\t" +
                            string.Join("," + Environment.NewLine + "\t", array.Expressions.Select(e => converter.ToString(e).Replace(name, string.Empty)).ToArray());
                    }
            }
            throw new NotSupportedException();
        }
    }
}

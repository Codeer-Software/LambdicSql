using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class InsertIntoClause
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            var insertTargets = new List<string>();
            foreach (var m in methods)
            {
                list.Add(MethodToString(converter, m, insertTargets));
            }
            return string.Join(string.Empty, list.ToArray());
        }

        //TODO refactoring.
        static string MethodToString(ISqlStringConverter converter, MethodCallExpression method, List<string> insertTargets)
        {
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.InsertInto):
                    {
                        var table = FromClause.ExpressionToTableName(converter, method.Arguments[0]);
                        //TODO  table = converter.ToString(method.Arguments[0]) <- beset!
                        var arg = converter.ToString(method.Arguments[1]).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => GetColumnOnly(e)).ToArray();
                        insertTargets.AddRange(arg);
                        return Environment.NewLine + "INSERT INTO " + table + "(" + string.Join(", ", arg) + ")";
                    }
                case nameof(LambdicSql.Keywords.Values):
                    {
                        if (method.Arguments[1] is NewArrayExpression)
                        {
                            return Environment.NewLine + "\tVALUES (" + converter.ToString(method.Arguments[1]) + ")";
                        }
                        throw new NotSupportedException();
                    }
            }
            throw new NotSupportedException();
        }

        static string GetColumnOnly(string src)
        {
            var index = src.LastIndexOf(".");
            return index == -1 ? src : src.Substring(index + 1);
        }
    }
}

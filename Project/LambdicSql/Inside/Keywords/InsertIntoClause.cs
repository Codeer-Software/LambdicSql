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

                        if (method.Arguments.Count == 1)
                        {
                            var select = ObjectCreateAnalyzer.MakeSelectInfo(method.Arguments[0]);
                            var argAll = select.Elements.Select(e => e.Name).ToArray();
                            insertTargets.AddRange(argAll);
                            return Environment.NewLine + "INSERT INTO " + table + "(" + string.Join(", ", argAll) + ")";
                        }
                        else
                        {
                            var arg = converter.ToString(method.Arguments[1]).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => GetColumnOnly(e)).ToArray();
                            insertTargets.AddRange(arg);
                            return Environment.NewLine + "INSERT INTO " + table + "(" + string.Join(", ", arg) + ")";
                        }
                    }
                case nameof(LambdicSql.Keywords.Values):
                    {
                        if (method.Arguments[1] is NewArrayExpression)
                        {
                            return Environment.NewLine + "\tVALUES (" + converter.ToString(method.Arguments[1]) + ")";
                        }
                        else if (typeof(IParamInfo).IsAssignableFrom(method.Arguments[1].Type))
                        {
                            var obj = converter.ToObject(method.Arguments[1]);
                            var type = method.Arguments[1].Type;

                            var values = new List<string>();
                            foreach (var e in insertTargets)
                            {
                                var val = type.GetPropertyValue(e, obj);
                                var param = val as DbParam;
                                if (param != null)
                                {
                                    val = param.Value;
                                }
                                var text = converter.Context.Parameters.Push(val, e, null, param);
                                values.Add(text);
                            }
                            return Environment.NewLine + "\tVALUES (" + string.Join(", ", values.ToArray()) + ")";
                        }
                        else
                        { 
                            var obj = converter.ToObject(method.Arguments[1]);
                            var type = method.Arguments[1].Type;

                            var values = new List<string>();
                            foreach (var e in insertTargets)
                            {
                                var val = type.GetPropertyValue(e, obj);
                                var text = converter.Context.Parameters.Push(val, e, null, null);
                                values.Add(text);
                            }
                            return Environment.NewLine + "\tVALUES (" + string.Join(", ", values.ToArray()) + ")";
                        }
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

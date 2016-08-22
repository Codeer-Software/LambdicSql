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
                        var arg = converter.ToString(method.Arguments[1]).Split(',').Select(e => GetColumnOnly(e)).ToArray();
                        insertTargets.AddRange(arg);
                        return Environment.NewLine + "INSERT INTO " + converter.ToString(method.Arguments[0]) + "(" + string.Join(", ", arg) + ")";
                    }
                case nameof(LambdicSql.Keywords.InsertIntoExcepting):
                    {
                        var select = ObjectCreateAnalyzer.MakeSelectInfo(method.Arguments[0]);

                        var array = method.Arguments[1] as NewArrayExpression;
                        var exclusions = array.Expressions.Select(e => converter.ToString(e)).ToList();
                        var arg = select.Elements.Select(e => e.Name).
                            Where(e=>!exclusions.Any(ee=>ee == e)).ToArray();
                        insertTargets.AddRange(arg);
                        return Environment.NewLine + "INSERT INTO " + converter.ToString(method.Arguments[0]) + "(" + string.Join(", ", arg) + ")";
                    }
                case nameof(LambdicSql.Keywords.Values):
                    {
                        if (method.Arguments[1] is NewArrayExpression)
                        {
                            return Environment.NewLine + "\tVALUES (" + converter.ToString(method.Arguments[1]) + ")";
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
                case nameof(LambdicSql.Keywords.ValuesWithTypes):
                    {
                        var obj = converter.ToObject(method.Arguments[1]);
                        var type = method.Arguments[1].Type;

                        var values = new List<string>();
                        foreach (var e in insertTargets)
                        {
                            var val = type.GetPropertyValue(e, obj);
                            var param = type.GetPropertyParamType(e);
                            var text = converter.Context.Parameters.Push(val, e, null, param);
                            values.Add(text);
                        }
                        return Environment.NewLine + "\tVALUES (" + string.Join(", ", values.ToArray()) + ")";
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

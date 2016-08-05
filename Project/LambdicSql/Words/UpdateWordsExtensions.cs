using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class UpdateWordsExtensions
    {
        public interface IUpdateAfter<TSelected, T> : ISqlKeyWord<TSelected> where T : class { }
        public static IUpdateAfter<TSelected, T> Update<TSelected, T>(this ISqlKeyWord<TSelected> words, T table) where T : class => null;
        public static ISqlKeyWord<TSelected> Set<TSelected, T>(this IUpdateAfter<TSelected, T> words, T setting) where T :class => null;

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
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
                case nameof(Update): return Environment.NewLine + "UPDATE " + converter.ToString(method.Arguments[1]);
                case nameof(Set):
                    {
                        var select = SelectDefineAnalyzer.MakeSelectInfo(method.Arguments[1]);
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

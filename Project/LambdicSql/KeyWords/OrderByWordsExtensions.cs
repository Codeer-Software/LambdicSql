using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    //TODO style change?
    public static class OrderByWordsExtensions
    {
        public interface IOrderByAfter<T> : ISqlKeyWord<T> { }

        public static IOrderByAfter<TSelected> OrderBy<TSelected>(this ISqlKeyWord<TSelected> words) => InvalitContext.Throw<IOrderByAfter<TSelected>>(nameof(OrderBy));
        public static IOrderByAfter<TSelected> ASC<TSelected, T>(this IOrderByAfter<TSelected> words, T target) => InvalitContext.Throw<IOrderByAfter<TSelected>>(nameof(ASC));
        public static IOrderByAfter<TSelected> DESC<TSelected, T>(this IOrderByAfter<TSelected> words, T target) => InvalitContext.Throw<IOrderByAfter<TSelected>>(nameof(DESC));

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            foreach (var m in methods.Skip(1))
            {
                var argSrc = m.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(m.Method.Name, argSrc));
            }
            return Environment.NewLine + "ORDER BY" + string.Join(",", list.ToArray());
        }

        static string MethodToString(string name, string[] argSrc)
        {
            switch (name)
            {
                case nameof(OrderBy): return Environment.NewLine + "ORDER BY";
                case nameof(ASC): return Environment.NewLine + "\t" + argSrc[0] + " ASC";
                case nameof(DESC): return Environment.NewLine + "\t" + argSrc[0] + " DESC";
            }
            throw new NotSupportedException();
        }
    }
}

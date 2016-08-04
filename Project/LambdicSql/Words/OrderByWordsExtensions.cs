using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class OrderByWordsExtensions
    {
        public static ISqlKeyWord<TSelected> OrderBy<TSelected>(this ISqlKeyWord<TSelected> words) => null;
        public static ISqlKeyWord<TSelected> ASC<TSelected>(this ISqlKeyWord<TSelected> words, object target) => null;
        public static ISqlKeyWord<TSelected> DESC<TSelected>(this ISqlKeyWord<TSelected> words, object target) => null;

        public static string MethodChainToString(ISqlStringConverter converter, MethodCallExpression[] methods)
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

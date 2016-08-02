using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;
using LambdicSql.Inside;
using System.Linq;
using System.Collections.Generic;

namespace LambdicSql
{
    public static class SelectWordsExtensions
    {
        public static ISqlWords<TSelected> Select<TSelected>(this ISqlWords words, AggregatePredicate predicate, TSelected selected) => null;
        public static ISqlWords<TSelected> Select<TSelected>(this ISqlWords words, TSelected selected) => null;

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            Expression define = null;
            AggregatePredicate? aggregatePredicate = null;
            if (method.Arguments[1].Type == typeof(AggregatePredicate))
            {
                aggregatePredicate = (AggregatePredicate)((ConstantExpression)method.Arguments[1]).Value;
                define = method.Arguments[2];
            }
            else
            {
                define = method.Arguments[1];
            }

            var select = SelectDefineAnalyzer.MakeSelectInfo(method.Arguments[1]);
            select.SetPredicate(aggregatePredicate);
            select.SelectedType = method.Method.ReturnType;
            if (converter.DbInfo.SelectClause == null)
            {
                converter.DbInfo.SelectClause = select;
                converter.DbInfo.SelectClause.Define = define;
            }
            var text = select.ToString(converter);
            return Environment.NewLine + text;
        }
    }


    public static class FromWordsExtensions
    {
        public static ISqlWords<TSelected> From<TSelected,T>(this ISqlWords<TSelected> words, T tbale) => null;
        public static ISqlWords<TSelected> Join<TSelected, T>(this ISqlWords<TSelected> words, T tbale, bool condition) => null;
        public static ISqlWords<TSelected> LeftJoin<TSelected, T>(this ISqlWords<TSelected> words, T tbale, bool condition) => null;
        public static ISqlWords<TSelected> RightJoin<TSelected, T>(this ISqlWords<TSelected> words, T tbale, bool condition) => null;
        public static ISqlWords<TSelected> CrossJoin<TSelected, T>(this ISqlWords<TSelected> words, T tbale) => null;

        public static string MethodChainToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            foreach (var m in methods)
            {
                var argSrc = m.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(m.Method.Name, argSrc));
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(string name, string[] argSrc)
        {
            switch (name)
            {
                case nameof(From): return Environment.NewLine + "FROM " + argSrc[0];
                case nameof(CrossJoin): return Environment.NewLine + "\tCROSS JOIN " + argSrc[0];
            }
            return Environment.NewLine + "\t" + name.ToUpper() + " " + argSrc[0] + " ON " + argSrc[1];
        }
    }

    public static class WhereWordsExtensions
    {
        public static ISqlWords<TSelected> Where<TSelected>(this ISqlWords<TSelected> words, bool condition) => null;

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            return Environment.NewLine + "WHERE " + converter.ToString(method.Arguments[1]);
        }
    }

    public static class GroupByWordsExtensions
    {
        public static ISqlWords<TSelected> GroupBy<TSelected>(this ISqlWords<TSelected> words, params object[] target) => null;

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            return Environment.NewLine + "GROUP BY " + converter.ToString(method.Arguments[1]);
        }
    }


    public static class OrderByWordsExtensions
    {
        public static ISqlWords<TSelected> OrderBy<TSelected>(this ISqlWords<TSelected> words) => null;
        public static ISqlWords<TSelected> ASC<TSelected>(this ISqlWords<TSelected> words, object target) => null;
        public static ISqlWords<TSelected> DESC<TSelected>(this ISqlWords<TSelected> words, object target) => null;

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
                case nameof(ASC): return Environment.NewLine + "\t" +  argSrc[0] + " ASC";
                case nameof(DESC): return Environment.NewLine + "\t" + argSrc[0] + " DESC";
            }
            throw new NotSupportedException();
        }
    }

    public static class InsertIntoWordsExtensions
    {
        public static ISqlWords<TSelected> InsertInto<TSelected>(this ISqlWords<TSelected> words, object table, params object[] targets) => null;
        public static ISqlWords<TSelected> Values<TSelected>(this ISqlWords<TSelected> words, params object[] targets) => null;

        public static string MethodChainToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            foreach (var m in methods)
            {
                var argSrc = m.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(m.Method.Name, argSrc));
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(string name, string[] argSrc)
        {
            switch (name)
            {
                case nameof(InsertInto): return Environment.NewLine + "INSERT INTO " + argSrc[0] + "(" + string.Join(", ", argSrc.Skip(1).ToArray()) + ")";
                case nameof(Values): return Environment.NewLine + "\tVALUES (" + string.Join(", ", argSrc) + ")";
            }
            throw new NotSupportedException();
        }
    }

    public static class UpdateWordsExtensions
    {
        public static ISqlWords<TSelected> Update<TSelected>(this ISqlWords<TSelected> words, object table) => null;
        public static ISqlWords<TSelected> Set<TSelected>(this ISqlWords<TSelected> words) => null;
        public static ISqlWords<TSelected> Assign<TSelected>(this ISqlWords<TSelected> words, object target, object value) => null;

        public static string MethodChainToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            for (int i = 0; i < methods.Length; i++)
            {
                var m = methods[i];
                var argSrc = m.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(m.Method.Name, argSrc));
                if (i + 1 < methods.Length)
                {
                    switch (methods[i].Method.Name)
                    {
                        case nameof(Assign):
                            switch (methods[i + 1].Method.Name)
                            {
                                case nameof(Assign):
                                    list.Add(", ");
                                    break;
                            }
                            break;
                    }
                }
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(string name, string[] argSrc)
        {
            switch (name)
            {
                case nameof(Update): return Environment.NewLine + "UPDATE " + argSrc[0];
                case nameof(Set): return Environment.NewLine + "SET";
                case nameof(Assign): return Environment.NewLine + "\t" + argSrc[0] + " = " + argSrc[1];
            }
            throw new NotSupportedException();
        }
        /*
        static string MemberNameOnly(string src)
        {
            var index = src.LastIndexOf(".");
            return index == -1 ? src : src.Substring(index + 1);
        }
        */
    }
}

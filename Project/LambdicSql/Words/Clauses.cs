using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;
using LambdicSql.Inside;
using System.Linq;
using System.Collections.Generic;
using LambdicSql.Words;

namespace LambdicSql
{
    public static class SelectWordsExtensions
    {
        public static ISqlWords<TSelected> Select<TSelected>(this ISqlSyntax words, AggregatePredicate predicate, TSelected selected) => null;
        public static ISqlWords<TSelected> Select<TSelected>(this ISqlSyntax words, TSelected selected) => null;
        public static ISqlWords<TSelected> SelectFrom<TSelected>(this ISqlSyntax words, TSelected selected) => null;
        
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

            var select = SelectDefineAnalyzer.MakeSelectInfo(define);
           
            if (converter.DbInfo.SelectClause == null)
            {
                converter.DbInfo.SelectClause = select;
                converter.DbInfo.SelectClause.Define = define;
            }
            var text = ToString(GetPredicate(aggregatePredicate), select.GetElements(), converter);
            if (method.Method.Name == nameof(SelectFrom))
            {
                text = text + Environment.NewLine + "FROM " + converter.ToString(method.Arguments[1]);
            }
            return Environment.NewLine + text;
        }

        static string ToString(string _predicate, SelectElement[] _elements, ISqlStringConverter decoder)
            => "SELECT" + _predicate + Environment.NewLine + "\t" +
            string.Join("," + Environment.NewLine + "\t", _elements.Select(e => ToString(decoder, e)).ToArray());

        static string ToString(ISqlStringConverter decoder, SelectElement element)
            => element.Expression == null ? element.Name : decoder.ToString(element.Expression) + " AS \"" + element.Name + "\"";

        static string GetPredicate(AggregatePredicate? aggregatePredicate)
        {
            if (aggregatePredicate == null)
            {
                return string.Empty;
            }
            switch (aggregatePredicate)
            {
                case AggregatePredicate.All:
                    return " ALL";
                case AggregatePredicate.Distinct:
                    return " DISTINCT";
            }
            return string.Empty;
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
                list.Add(MethodToString(converter, m));
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            string name = method.Method.Name;
            string[] argSrc = method.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();//TODO
            switch (name)
            {
                case nameof(From): return Environment.NewLine + "FROM " + ExpressionToTableName(converter, method.Arguments[1]);
                case nameof(CrossJoin): return Environment.NewLine + "\tCROSS JOIN " + ExpressionToTableName(converter, method.Arguments[1]);
            }
            return Environment.NewLine + "\t" + name.ToUpper() + " " + ExpressionToTableName(converter, method.Arguments[1]) + " ON " + argSrc[1];
        }

        static string ExpressionToTableName(ISqlStringConverter decoder, Expression exp)
        {
            var text = decoder.ToString(exp);
            var methodCall = exp as MethodCallExpression;
            if (methodCall != null)
            {
                //TODO if cast expression
                //TODO oracl custom
                var x = ((MemberExpression)methodCall.Arguments[0]).Member.Name;
                return text + " AS " + x;
            }

            var table = decoder.DbInfo.GetLambdaNameAndTable()[text];
            if (table.SubQuery == null)
            {
                return table.SqlFullName;
            }
            
            //TODO no need
            return decoder.ToString(table.SubQuery) + " AS " + table.SqlFullName;
        }
    }

    public static class WhereWordsExtensions
    {
        public static ISqlWords<TSelected> Where<TSelected>(this ISqlWords<TSelected> words, bool condition) => null;

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            var text = converter.ToString(method.Arguments[1]);
            return string.IsNullOrEmpty(text.Trim()) ? string.Empty : Environment.NewLine + "WHERE " + converter.ToString(method.Arguments[1]);
        }
    }

    public static class ConditionWordsExtensions
    {
        public static bool Condition(this ISqlHelper words, bool enable, bool condition) => default(bool);

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            object obj;
            ExpressionToObject.GetExpressionObject(method.Arguments[1], out obj);
            return (bool)obj ? converter.ToString(method.Arguments[2]) : string.Empty;
        }
    }


    //ISqlHelper

    public static class HavingWordsExtensions
    {
        public static ISqlWords<TSelected> Having<TSelected>(this ISqlWords<TSelected> words, bool condition) => null;

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            var text = converter.ToString(method.Arguments[1]);
            return string.IsNullOrEmpty(text.Trim()) ? string.Empty : Environment.NewLine + "HAVING " + converter.ToString(method.Arguments[1]);
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
                case nameof(InsertInto):
                {
                    var arg = argSrc.Last().Split(',').Select(e => GetColumnOnly(e)).ToArray();
                    return Environment.NewLine + "INSERT INTO " + argSrc[0] + "(" + string.Join(", ", arg) + ")";

                }
                case nameof(Values): return Environment.NewLine + "\tVALUES (" + string.Join(", ", argSrc) + ")";
            }
            throw new NotSupportedException();
        }

        static string GetColumnOnly(string src)
        {
            var index = src.LastIndexOf(".");
            return index == -1 ? src : src.Substring(index + 1);
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
                case nameof(Assign): return Environment.NewLine + "\t" + MemberNameOnly(argSrc[0]) + " = " + argSrc[1];
            }
            throw new NotSupportedException();
        }
        
        static string MemberNameOnly(string src)
        {
            var index = src.LastIndexOf(".");
            return index == -1 ? src : src.Substring(index + 1);
        }
        
    }

    public static class DeleteWordsExtensions
    {
        public static ISqlWords<TSelected> Delete<TSelected>(this ISqlWords<TSelected> words) => null;

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            return Environment.NewLine + "DELETE";
        }
    }
}

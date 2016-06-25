using LambdicSql.QueryInfo;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    static class QueryAnalyzer
    {
        //I maybe change sql text by dbConnection type. 
        internal static string MakeQueryString(IQueryInfo query, Type dbConnection)
        {
            return string.Join(Environment.NewLine, new[] {
                ToString(Adjust(query.Db, query.Select)),
                ToString(Adjust(query.Db, query.From)),
                ToString(query.Where),
                ToString(query.GroupBy),
                ToString(query.OrderBy)
            });
        }

        static SelectInfo Adjust(DbInfo db, SelectInfo select)
        {
            if (select != null)
            {
                return select;
            }
            select = new SelectInfo(db.GetAllColumns());
            foreach (var e in select.DbColumns)
            {
                select.Add(e.Key, SelectElementInfo.DbColumnElement(e.Key), e.Value);
            }
            return select;
        }

        static FromInfo Adjust(DbInfo db, FromInfo from)
        {
            if (from != null)
            {
                return from;
            }

            //table count must be 1.
            if (db.Children.Count != 1 || db.Children.First().Value.Children.Count != 1)
            {
                throw new NotSupportedException();
            }
            return new FromInfo(db.Children.First().Value.Children.First().Value.FullNameText);
        }

        static string ToString(SelectInfo selectInfo)
            => string.Join(Environment.NewLine + "\t", new[] { "SELECT" }.Concat(selectInfo.AliasElements.Select(e => ToString(e)))) + Environment.NewLine;

        static string ToString(KeyValuePair<string, SelectElementInfo> element)
            => ToString(element.Value) + " AS " + element.Key;

        static string ToString(SelectElementInfo value)
        {
            if (value.IsDbColumn)
            {
                return value.DbColumn;
            }
            return value.Function + "(" + MakeSqlArguments(value.Arguments) + ")";
        }

        //TODO check arguments format.
        static string MakeSqlArguments(IEnumerable<object> src)
        {
            var result = new List<string>();
            foreach (var arg in src)
            {
                var col = arg as ColumnInfo;
                result.Add(col == null ? "'" + arg.ToString() + "'" : col.FullNameText); 
            }
            return string.Join(", ", result);
        }

        static string ToString(FromInfo fromInfo)
            => string.Join(Environment.NewLine + "\t", new[] { "FROM " + fromInfo.MainTable }.Concat(fromInfo.Joins.Select(e=>ToString(e)))) + Environment.NewLine;

        static string ToString(JoinInfo join)
            => join.JoinTable + " ON " + ToString(join.Condition);

        static string ToString(BinaryExpression condition)
        {
            return condition.ToString();//TODO need convert.
        }

        static string ToString(WhereInfo whereInfo)
            => string.Join(Environment.NewLine + "\t", new[] { "WHERE" }.Concat(whereInfo.Conditions.Select((e, i) => ToString(e, i)))) + Environment.NewLine;

        static string ToString(IConditionInfo condition, int index)
        {
            string text;
            if (condition.GetType() == typeof(ConditionInfoBinary)) text = ToString((ConditionInfoBinary)condition);
            if (condition.GetType() == typeof(ConditionInfoIn)) text = ToString((ConditionInfoIn)condition);
            if (condition.GetType() == typeof(ConditionInfoLike)) text = ToString((ConditionInfoLike)condition);
            else throw new NotSupportedException();

            var connection = index == 0 ? string.Empty :
                             condition.ConditionConnection == ConditionConnection.And ? "AND " : "OR ";
            var not = condition.IsNot ? "NOT " : string.Empty;
            return connection + not + text;
        }

        static string ToString(ConditionInfoBinary condition)
            => ToString(condition.Expression);

        static string ToString(ConditionInfoIn condition)
            => condition.Target + " IN(" + MakeSqlArguments(condition.Arguments) + ")";

        static string ToString(ConditionInfoLike condition)
            => condition.Target + " LIKE " + condition.SearchString;

        static string ToString(GroupByInfo groupBy)
            => string.Join(Environment.NewLine + "\t", new[] { "GROUP BY" }.Concat(groupBy.Elements)) + Environment.NewLine;

        static string ToString(OrderByInfo orderBy)
            => string.Join(Environment.NewLine + "\t", new[] { "ORDER BY" }.Concat(orderBy.Elements.Select(e=>ToString(e)))) + Environment.NewLine;

        private static object ToString(OrderByElement element)
            => element.Target + " " + element.Order;
    }
}

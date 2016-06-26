using LambdicSql.QueryInfo;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    class QueryToSql
    {
        DbInfo _db;
        
        internal string MakeQueryString(IQueryInfo query)
        {
            _db = query.Db;
            return string.Join(Environment.NewLine, new[] {
                ToString(Adjust(query.Select)),
                ToString(Adjust(query.From)),
                ToString(query.Where),
                ToString(query.GroupBy),
                ToString(query.OrderBy)
            }.Where(e=>!string.IsNullOrEmpty(e)));
        }

        SelectInfo Adjust(SelectInfo select)
        {
            if (select != null)
            {
                return select;
            }
            select = new SelectInfo();
            foreach (var e in _db.LambdaNameAndColumn)
            {
                select.Add(new SelectElementInfo(e.Key + " AS " + e.Key.Replace(".", "@"), null));//TODO@ special spec
            }
            return select;
        }

        FromInfo Adjust(FromInfo from)
        {
            if (from != null)
            {
                return from;
            }

            //table count must be 1.
            if (_db.LambdaNameAndTable.Count != 1)
            {
                throw new NotSupportedException();
            }
            return new FromInfo(_db.LambdaNameAndTable.First().Value);
        }

        string ToString(SelectInfo selectInfo)
            => "SELECT " + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", selectInfo.Elements.Select(e => ToString(e))) + Environment.NewLine;

        string ToString(SelectElementInfo element)
            => element.Expression == null ? element.Name : ToString(element.Expression) + " AS " + element.Name;

        string ToString(Expression exp)
            => ExpressionToSqlString.ToString(_db, exp);

        string MakeSqlArguments(IEnumerable<object> src)
        {
            var result = new List<string>();
            foreach (var arg in src)
            {
                var col = arg as ColumnInfo;
                result.Add(col == null ? "'" + arg.ToString() + "'" : col.SqlFullName); 
            }
            return string.Join(", ", result);
        }

        string ToString(FromInfo fromInfo)
            => string.Join(Environment.NewLine + "\t", new[] { "FROM " + fromInfo.MainTable.SqlFullName }.Concat(fromInfo.Joins.Select(e=>ToString(e)))) + Environment.NewLine;

        string ToString(JoinInfo join)
            => "JOIN " + join.JoinTable.SqlFullName + " ON " + ToString(join.Condition);
        
        string ToString(WhereInfo whereInfo)
            => whereInfo == null ?
                string.Empty:
                string.Join(Environment.NewLine + "\t", new[] { "WHERE" }.Concat(whereInfo.Conditions.Select((e, i) => ToString(e, i)))) + Environment.NewLine;

        string ToString(IConditionInfo condition, int index)
        {
            string text;
            var type = condition.GetType();
            if (type == typeof(ConditionInfoExpression)) text = ToString((ConditionInfoExpression)condition);
            else if (type == typeof(ConditionInfoIn)) text = ToString((ConditionInfoIn)condition);
            else if (type == typeof(ConditionInfoLike)) text = ToString((ConditionInfoLike)condition);
            else if (type == typeof(ConditionInfoBetween)) text = ToString((ConditionInfoBetween)condition);
            else throw new NotSupportedException();

            var connection = index == 0 ? string.Empty :
                             condition.ConditionConnection == ConditionConnection.And ? "AND " : "OR ";
            var not = condition.IsNot ? "NOT " : string.Empty;
            return connection + not + text;
        }

        string ToString(ConditionInfoBetween condition)
            => ToString(condition.Target) + " BETWEEN '" + condition.Min + "' AND '" + condition.Max + "'";//TODO@ think db column order.

        string ToString(ConditionInfoExpression condition)
            => ToString(condition.Expression);

        string ToString(ConditionInfoIn condition)
            => ToString(condition.Target) + " IN(" + MakeSqlArguments(condition.Arguments) + ")";//TODO@ think db column order.

        string ToString(ConditionInfoLike condition)
            => ToString(condition.Target) + " LIKE '" + condition.SearchText + "'";//TODO@ think db column order.

        string ToString(GroupByInfo groupBy)
            => groupBy == null ? 
                string.Empty :
                "GROUP BY " + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", groupBy.Elements.Select(e=>ToString(e))) + Environment.NewLine;

        string ToString(OrderByInfo orderBy)
            => orderBy == null ?
                string.Empty :
                "ORDER BY " + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", orderBy.Elements.Select(e=>ToString(e))) + Environment.NewLine;

        private object ToString(OrderByElement element)
            => ToString(element.Target) + " " + element.Order;
    }
}

using LambdicSql.Inside;
using LambdicSql.QueryInfo;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public class QueryParser
    {
        DbInfo _db;
        ExpressionParser _parser;

        public string ToString(IQueryInfo query)
        {
            return ToStringCore(query) + ";";
        }

        public virtual string CustomOperator(Type type1, string @operator, Type type2) => @operator;

        internal string ToStringCore(IQueryInfo query)
        {
            //TODO@@ init query info.

            _db = query.Db;
            _parser = new ExpressionParser(_db, this);
            return string.Join(Environment.NewLine, new[] {
                ToString(Adjust(query.Select)),
                ToString(Adjust(query.From)),
                ToString(query.Where, "WHERE"),
                ToString(query.GroupBy),
                ToString(query.Having, "HAVING"),
                ToString(query.OrderBy)
            }.Where(e => !string.IsNullOrEmpty(e)).ToArray());
        }

        SelectInfo Adjust(SelectInfo select)
        {
            if (select != null)
            {
                return select;
            }
            select = new SelectInfo();
            foreach (var e in _db.GetLambdaNameAndColumn())
            {
                select.Add(new SelectElementInfo(e.Key, null));
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
            if (_db.GetLambdaNameAndTable().Count != 1)
            {
                throw new NotSupportedException();
            }
            return new FromInfo(_db.GetLambdaNameAndTable().First().Value);
        }

        string ToString(SelectInfo selectInfo)
            => "SELECT" + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", selectInfo.GetElements().Select(e => ToString(e)).ToArray());

        string ToString(SelectElementInfo element)
            => element.Expression == null ? element.Name : ToString(element.Expression) + " AS \"" + element.Name + "\"";

        string ToString(Expression exp)
            => _parser.ToString(exp).Text;

        string ToString(FromInfo fromInfo)
            => "FROM" + Environment.NewLine + "\t" + string.Join(Environment.NewLine + "\t", new[] { fromInfo.MainTable.SqlFullName }.Concat(fromInfo.GetJoins().Select(e => ToString(e))).ToArray());

        string ToString(JoinInfo join)
            => "JOIN " + join.JoinTable.SqlFullName + " ON " + ToString(join.Condition);

        string ToString(ConditionClauseInfo whereInfo, string clause)
            => (whereInfo == null || whereInfo.ConditionCount == 0) ?
                string.Empty :
                string.Join(Environment.NewLine + "\t", new[] { clause }.Concat(whereInfo.GetConditions().Select((e, i) => ToString(e, i))).ToArray());

        string ToString(IConditionInfo condition, int index)
        {
            string text;
            var type = condition.GetType();
            if (type == typeof(ConditionInfoExpression)) text = ToString((ConditionInfoExpression)condition);
            else if (type == typeof(ConditionInfoIn)) text = ToString((ConditionInfoIn)condition);
            else if (type == typeof(ConditionInfoLike)) text = ToString((ConditionInfoLike)condition);
            else if (type == typeof(ConditionInfoBetween)) text = ToString((ConditionInfoBetween)condition);
            else throw new NotSupportedException();

            var connection = string.Empty;
            if (index != 0)
            {
                switch (condition.ConditionConnection)
                {
                    case ConditionConnection.And: connection = "AND "; break;
                    case ConditionConnection.Or: connection = "OR "; break;
                    default: throw new NotSupportedException();
                }
            }
            var not = condition.IsNot ? "NOT " : string.Empty;
            return connection + not + text;
        }

        string ToString(ConditionInfoBetween condition)
            => ToString(condition.Target) + " BETWEEN " + _parser.ToStringObject(condition.Min) + " AND " + _parser.ToStringObject(condition.Max);

        string ToString(ConditionInfoExpression condition)
            => ToString(condition.Expression);

        string ToString(ConditionInfoIn condition)
            => ToString(condition.Target) + " IN(" + _parser.MakeSqlArguments(condition.GetArguments()) + ")";

        string ToString(ConditionInfoLike condition)
            => ToString(condition.Target) + " LIKE " + _parser.ToStringObject(condition.SearchText);

        string ToString(GroupByInfo groupBy)
            => (groupBy == null || groupBy.GetElements().Length == 0) ?
                string.Empty :
                "GROUP BY " + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", groupBy.GetElements().Select(e => ToString(e)).ToArray());

        string ToString(OrderByInfo orderBy)
            => (orderBy == null || orderBy.GetElements().Length == 0) ?
                string.Empty :
                "ORDER BY " + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", orderBy.GetElements().Select(e => ToString(e)).ToArray());

        string ToString(OrderByElement element)
            => ToString(element.Target) + " " + element.Order;
    }
}

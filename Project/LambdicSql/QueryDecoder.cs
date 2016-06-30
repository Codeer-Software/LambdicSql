using LambdicSql.Inside;
using LambdicSql.QueryInfo;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    //@@@各句Infoに文字列化の能力を分散するかなー。
    //→じゃあInfoではなく句っていう名前になるかなー。
    public class QueryDecoder
    {
        DbInfo _db;
        ExpressionDecoder _parser;

        public string ToString(IQueryInfo query)
        {
            return ToStringCore(query) + ";";
        }

        public virtual string CustomOperator(Type type1, string @operator, Type type2) => @operator;

        //@@@ Custom句Info?→いやーいらんか。　★じゃあ、これを継承じゃなくてもよくなってきたなー。IQueryDecodeCustomerとかにするか？
        //★もっといい感じになるやろ。

        protected string ExpressionToString(Expression exp) => _parser.ToString(exp).Text;

        internal string ToStringCore(IQueryInfo query)
        {
            //TODO@@ init query info.

            _db = query.Db;
            _parser = new ExpressionDecoder(_db, this);
            return string.Join(Environment.NewLine, new[] {
                ToString(Adjust(query.Select)),
                ToString(Adjust(query.From)),
                ToString(query.Where, "WHERE"),
                ToString(query.GroupBy),
                ToString(query.Having, "HAVING"),
                ToString(query.OrderBy)
            }.Where(e => !string.IsNullOrEmpty(e)).ToArray());
        }

        SelectClause Adjust(SelectClause select)
        {
            if (select != null)
            {
                return select;
            }
            select = new SelectClause();
            foreach (var e in _db.GetLambdaNameAndColumn())
            {
                select.Add(new SelectElement(e.Key, null));
            }
            return select;
        }

        FromClause Adjust(FromClause from)
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
            return new FromClause(_db.GetLambdaNameAndTable().First().Value.SqlFullName);
        }

        string ToString(SelectClause selectInfo)
            => "SELECT" + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", selectInfo.GetElements().Select(e => ToString(e)).ToArray());

        string ToString(SelectElement element)
            => element.Expression == null ? element.Name : ExpressionToString(element.Expression) + " AS \"" + element.Name + "\"";
        
        string ToString(FromClause fromInfo)
        {
            string mainTable = string.IsNullOrEmpty(fromInfo.MainTableSqlFullName) ? ExpressionToTableName(fromInfo.MainTable) : fromInfo.MainTableSqlFullName;
            return "FROM" + Environment.NewLine + "\t" + string.Join(Environment.NewLine + "\t", new[] { mainTable }.Concat(fromInfo.GetJoins().Select(e => ToString(e))).ToArray());
        }

        string ToString(JoinClause join)
            => "JOIN " + ExpressionToTableName(join.JoinTable) + " ON " + ExpressionToString(join.Condition);

        string ToString(ConditionClause whereInfo, string clause)
            => (whereInfo == null || whereInfo.ConditionCount == 0) ?
                string.Empty :
                string.Join(Environment.NewLine + "\t", new[] { clause }.Concat(whereInfo.GetConditions().Select((e, i) => ToString(e, i))).ToArray());

        string ExpressionToTableName(Expression exp)
            => _db.GetLambdaNameAndTable()[ExpressionToString(exp)].SqlFullName;

        string ToString(ICondition condition, int index)
        {
            string text;
            var type = condition.GetType();
            if (type == typeof(ConditionExpression)) text = ToString((ConditionExpression)condition);
            else if (type == typeof(ConditionIn)) text = ToString((ConditionIn)condition);
            else if (type == typeof(ConditionLike)) text = ToString((ConditionLike)condition);
            else if (type == typeof(ConditionBetween)) text = ToString((ConditionBetween)condition);
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

        string ToString(ConditionBetween condition)
            => ExpressionToString(condition.Target) + " BETWEEN " + _parser.ToStringObject(condition.Min) + " AND " + _parser.ToStringObject(condition.Max);

        string ToString(ConditionExpression condition)
            => ExpressionToString(condition.Expression);

        string ToString(ConditionIn condition)
            => ExpressionToString(condition.Target) + " IN(" + _parser.MakeSqlArguments(condition.GetArguments()) + ")";

        string ToString(ConditionLike condition)
            => ExpressionToString(condition.Target) + " LIKE " + _parser.ToStringObject(condition.SearchText);

        string ToString(GroupByClause groupBy)
            => (groupBy == null || groupBy.GetElements().Length == 0) ?
                string.Empty :
                "GROUP BY " + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", groupBy.GetElements().Select(e => ExpressionToString(e)).ToArray());

        string ToString(OrderByClause orderBy)
            => (orderBy == null || orderBy.GetElements().Length == 0) ?
                string.Empty :
                "ORDER BY " + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", orderBy.GetElements().Select(e => ToString(e)).ToArray());

        string ToString(OrderByElement element)
            => ExpressionToString(element.Target) + " " + element.Order;
    }
}

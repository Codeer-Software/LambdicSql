using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Clause.From
{
    public class FromClause : IClause
    {
        List<JoinElement> _joins = new List<JoinElement>();
        bool _isUseSubQueryAs { get; } = true;

        public string MainTableSqlFullName { get; }
        public Expression MainTable { get; }
        public JoinElement[] GetJoins() => _joins.ToArray();

        public FromClause(string mainTableSqlFullName)
        {
            MainTableSqlFullName = mainTableSqlFullName;
        }

        public FromClause(Expression mainTable)
        {
            MainTable = mainTable;
        }

        public FromClause(string mainTableSqlFullName, Expression mainTable, JoinElement[] joins)
        {
            MainTableSqlFullName = mainTableSqlFullName;
            MainTable = mainTable;
            _joins = joins.ToList();
        }

        public IClause Clone()
        {
            var clone = string.IsNullOrEmpty(MainTableSqlFullName) ? new FromClause(MainTable) : new FromClause(MainTableSqlFullName);
            clone._joins.AddRange(_joins);
            return clone;
        }

        public string ToString(ISqlStringConverter decoder)
        {
            string mainTable = string.IsNullOrEmpty(MainTableSqlFullName) ? ExpressionToTableName(decoder, MainTable) : MainTableSqlFullName;
            return "FROM" + Environment.NewLine + "\t" + string.Join(Environment.NewLine + "\t", new[] { mainTable }.Concat(GetJoins().Select(e => ToString(decoder, e))).ToArray());
        }

        internal void Join(JoinElement join) => _joins.Add(join);

        string ToString(ISqlStringConverter decoder, JoinElement join)
        {
            var clause = string.Empty;
            switch (join.JoinType)
            {
                case JoinType.Join: clause = "JOIN"; break;
                case JoinType.LeftJoin: clause = "LEFT JOIN"; break;
                case JoinType.RightJoin: clause = "RIGHT JOIN"; break;
                case JoinType.CrossJoin:
                    return "CROSS JOIN " + ExpressionToTableName(decoder, join.JoinTable);
            }
            return clause + " " + ExpressionToTableName(decoder, join.JoinTable) + " ON " + decoder.ToString(join.Condition);
        }

        string ExpressionToTableName(ISqlStringConverter decoder, Expression exp)
        {
            var table = decoder.DbInfo.GetLambdaNameAndTable()[decoder.ToString(exp)];
            if (table.SubQuery == null)
            {
                return table.SqlFullName;
            }
            var nameSeparator = _isUseSubQueryAs ? " AS " : " ";
            return decoder.ToString(table.SubQuery) + nameSeparator + table.SqlFullName;
        }

        string GetJoinName(JoinType type)
        {
            switch (type)
            {
                case JoinType.Join: return "JOIN";
                case JoinType.LeftJoin: return "LEFT JOIN";
                case JoinType.RightJoin: return "RIGHT JOIN";
                case JoinType.CrossJoin: return "CROSS JOIN";
            }
            throw new NotSupportedException();
        }
    }
}

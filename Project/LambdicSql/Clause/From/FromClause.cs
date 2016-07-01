using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Clause.From
{
    //TODO sub query in From clauses
    public class FromClause : IClause
    {
        List<JoinElement> _join = new List<JoinElement>();

        public string MainTableSqlFullName { get; }
        public Expression MainTable { get; }
        public JoinElement[] GetJoins() => _join.ToArray();

        public FromClause(string mainTableSqlFullName)
        {
            MainTableSqlFullName = mainTableSqlFullName;
        }

        public FromClause(Expression mainTable)
        {
            MainTable = mainTable;
        }

        public IClause Clone()
        {
            var clone = string.IsNullOrEmpty(MainTableSqlFullName) ? new FromClause(MainTable) : new FromClause(MainTableSqlFullName);
            clone._join.AddRange(_join);
            return clone;
        }

        public string ToString(ISqlStringConverter decoder)
        {
            string mainTable = string.IsNullOrEmpty(MainTableSqlFullName) ? ExpressionToTableName(decoder, MainTable) : MainTableSqlFullName;
            return "FROM" + Environment.NewLine + "\t" + string.Join(Environment.NewLine + "\t", new[] { mainTable }.Concat(GetJoins().Select(e => ToString(decoder, e))).ToArray());
        }

        internal void Join(JoinElement join) => _join.Add(join);

        string ToString(ISqlStringConverter decoder, JoinElement join)
            => "JOIN " + ExpressionToTableName(decoder, join.JoinTable) + " ON " + decoder.ToString(join.Condition);

        string ExpressionToTableName(ISqlStringConverter decoder, Expression exp)
            => decoder.DbInfo.GetLambdaNameAndTable()[decoder.ToString(exp)].SqlFullName;
    }

}

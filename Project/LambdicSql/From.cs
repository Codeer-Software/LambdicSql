using LambdicSql.Inside;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public class FromClause : IClause
    {
        List<JoinClause> _join = new List<JoinClause>();

        public string MainTableSqlFullName { get; }
        public Expression MainTable { get; }
        public JoinClause[] GetJoins() => _join.ToArray();

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
            var clone = new FromClause(MainTable);
            clone._join.AddRange(_join);
            return clone;
        }

        internal void Join(JoinClause join) => _join.Add(join);

        public string ToString(IExpressionDecoder decoder)
        {
            string mainTable = string.IsNullOrEmpty(MainTableSqlFullName) ? ExpressionToTableName(decoder, MainTable) : MainTableSqlFullName;
            return "FROM" + Environment.NewLine + "\t" + string.Join(Environment.NewLine + "\t", new[] { mainTable }.Concat(GetJoins().Select(e => ToString(decoder, e))).ToArray());
        }

        string ToString(IExpressionDecoder decoder, JoinClause join)
            => "JOIN " + ExpressionToTableName(decoder, join.JoinTable) + " ON " + decoder.ToString(join.Condition);

        string ExpressionToTableName(IExpressionDecoder decoder, Expression exp)
            => decoder.DbInfo.GetLambdaNameAndTable()[decoder.ToString(exp)].SqlFullName;
    }

    public class JoinClause
    {
        public Expression JoinTable { get; }
        public Expression Condition { get; }

        public JoinClause(Expression joinTable, Expression condition)
        {
            JoinTable = joinTable;
            Condition = condition;
        }
    }

    public static class FromQueryExtensions
    {
        public static IQueryFrom<TDB, TSelect> From<TDB, TSelect, T>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, T>> table)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.From = new FromClause(table.Body));

        public static IQueryFrom<TDB, TSelect> Join<TDB, TSelect>(this IQueryFrom<TDB, TSelect> query, Expression<Func<TDB, object>> table, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.From.Join(new JoinClause(table.Body, condition.Body)));
    }

    public interface IQueryFrom<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}

﻿using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Clause.From
{
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
            var clone = new FromClause(MainTable);
            clone._join.AddRange(_join);
            return clone;
        }

        internal void Join(JoinElement join) => _join.Add(join);

        public string ToString(IExpressionDecoder decoder)
        {
            string mainTable = string.IsNullOrEmpty(MainTableSqlFullName) ? ExpressionToTableName(decoder, MainTable) : MainTableSqlFullName;
            return "FROM" + Environment.NewLine + "\t" + string.Join(Environment.NewLine + "\t", new[] { mainTable }.Concat(GetJoins().Select(e => ToString(decoder, e))).ToArray());
        }

        string ToString(IExpressionDecoder decoder, JoinElement join)
            => "JOIN " + ExpressionToTableName(decoder, join.JoinTable) + " ON " + decoder.ToString(join.Condition);

        string ExpressionToTableName(IExpressionDecoder decoder, Expression exp)
            => decoder.DbInfo.GetLambdaNameAndTable()[decoder.ToString(exp)].SqlFullName;
    }

}
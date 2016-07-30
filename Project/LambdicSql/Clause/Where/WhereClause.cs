using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Clause.Where
{
    public class WhereClause : IClause
    {
        Expression _exp;
        public WhereClause(Expression exp) { _exp = exp; }
        public IClause Clone() => this;
        public string ToString(ISqlStringConverter decoder)
        {
            var text = decoder.ToString(_exp);
            if (string.IsNullOrEmpty(text.Replace("(", string.Empty).Replace(")", string.Empty).Trim())) return string.Empty;
            return "WHERE" + Environment.NewLine + "\t" + text;
        }
    }
}

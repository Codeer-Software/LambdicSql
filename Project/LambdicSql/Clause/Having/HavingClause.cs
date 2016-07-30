using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Clause.Having
{
    public class HavingClause : IClause
    {
        Expression _exp;
        public HavingClause(Expression exp) { _exp = exp; }
        public IClause Clone() => this;
        public string ToString(ISqlStringConverter decoder)
        {
            var text = decoder.ToString(_exp);
            if (string.IsNullOrEmpty(text.Replace("(", string.Empty).Replace(")", string.Empty).Trim())) return string.Empty;
            return "HAVING" + Environment.NewLine + "\t" + text;
        }
    }
}

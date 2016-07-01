using LambdicSql.QueryBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Clause.GroupBy
{
    public class GroupByClause : IClause
    {
        Expression[] _elements;
        public Expression[] GetElements() => _elements.ToArray();

        public GroupByClause(Expression[] elements)
        {
            _elements = elements;
        }

        public string ToString(IExpressionDecoder decoder)
            => GetElements().Length == 0 ?
                string.Empty :
                "GROUP BY " + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", GetElements().Select(e => decoder.ToString(e)).ToArray());

        public IClause Clone() => this;
    }
}

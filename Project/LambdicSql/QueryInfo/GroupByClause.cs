using System.Linq.Expressions;
using System.Linq;
using System;

namespace LambdicSql.QueryInfo
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

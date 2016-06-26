using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class GroupByInfo
    {
        public IReadOnlyList<Expression> Elements { get; }

        public GroupByInfo(IReadOnlyList<Expression> elements)
        {
            Elements = elements;
        }
    }
}

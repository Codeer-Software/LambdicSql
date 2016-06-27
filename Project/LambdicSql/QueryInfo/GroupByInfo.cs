using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class GroupByInfo
    {
        public List<Expression> Elements { get; }

        public GroupByInfo(List<Expression> elements)
        {
            Elements = elements;
        }
    }
}

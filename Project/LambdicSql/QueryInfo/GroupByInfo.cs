using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class GroupByInfo
    {
        public IReadOnlyList<string> Elements { get; }

        public GroupByInfo(IReadOnlyList<string> elements)
        {
            Elements = elements;
        }
    }
}

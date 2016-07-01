using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    public interface IExpressionDecoder
    {
        DbInfo DbInfo { get; }
        string ToString(Expression exp);

        //TODO check.
        string ToStringObject(object obj);
        string MakeSqlArguments(IEnumerable<object> args);
    }
}

using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class HavingClause : ConditionClause, IClause
    {
        public HavingClause() { }
        public HavingClause(Expression exp) : base(exp) { }

        public IClause Clone() => (HavingClause)Copy(new HavingClause());
        public string ToString(IExpressionDecoder decoder) => ToString(decoder, "HAVING");
    }
}

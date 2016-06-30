using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class WhereClause : ConditionClause, IClause
    {
        public WhereClause() { }
        public WhereClause(Expression exp) : base(exp) { }

        public IClause Clone() => (WhereClause)Copy(new WhereClause());
        public string ToString(IExpressionDecoder decoder) => ToString(decoder, "WHERE");
    }
}

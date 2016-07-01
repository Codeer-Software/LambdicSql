using LambdicSql.Clause.Condition;
using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql.Clause.Having
{
    public class HavingClause : ConditionClause, IClause
    {
        public HavingClause() { }
        public HavingClause(Expression exp) : base(exp) { }
        public IClause Clone() => (HavingClause)Copy(new HavingClause());
        public string ToString(IExpressionDecoder decoder) => ToString(decoder, "HAVING");
    }
}

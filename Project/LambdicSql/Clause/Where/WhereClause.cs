using LambdicSql.Clause.Condition;
using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql.Clause.Where
{
    public class WhereClause : ConditionClause, IClause
    {
        public WhereClause() { }
        public WhereClause(Expression exp) : base(exp) { }

        public IClause Clone() => (WhereClause)Copy(new WhereClause());
        public string ToString(ISqlStringConverter decoder) => ToString(decoder, "WHERE");
    }
}

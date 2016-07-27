using LambdicSql.QueryBase;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.Clause.Condition
{
    public class MultiCondition : ICondition
    {
        List<ICondition> _conditions;

        public MultiCondition(List<ICondition> conditions)
        {
            _conditions = conditions;
        }

        public bool IsNot { get; set; }
        public ConditionConnection ConditionConnection { get; set; }
        public string ToString(ISqlStringConverter decoder)
            => string.Join(" ", _conditions.Select((e, i) => ConditionClause.ToString(decoder, e, i)).ToArray());
    }
}

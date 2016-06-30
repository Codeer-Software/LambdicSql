using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    //TODO()

    public class ConditionClause
    {
        bool _isNotCore;
        ConditionConnection _nextConnectionCore;
        List<ICondition> _conditions = new List<ICondition>();

        bool IsNot
        {
            get
            {
                var isNot = _isNotCore;
                _isNotCore = false;
                return isNot;
            }
        }

        ConditionConnection NextConnection
        {
            get
            {
                var nextConnectionCore = _nextConnectionCore;
                _nextConnectionCore = ConditionConnection.Non;
                return nextConnectionCore;
            }
        }

        public int ConditionCount => _conditions.Count;
        public ICondition[] GetConditions() => _conditions.ToArray();

        public ConditionClause() { }

        public ConditionClause(Expression exp)
        {
            _conditions.Add(new ConditionExpression(false, ConditionConnection.And, exp));
        }

        public ConditionClause Clone()
        {
            var dst = new ConditionClause();
            dst._conditions.AddRange(_conditions);
            dst._isNotCore = _isNotCore;
            dst._nextConnectionCore = _nextConnectionCore;
            return dst;
        }

        internal void And(Expression exp)
            => _conditions.Add(new ConditionExpression(IsNot, ConditionConnection.And, exp));

        internal void Or(Expression exp)
            => _conditions.Add(new ConditionExpression(IsNot, ConditionConnection.Or, exp));

        internal void And()
            => _nextConnectionCore = ConditionConnection.And;

        internal void Or()
            => _nextConnectionCore = ConditionConnection.Or;

        internal void Not()
            => _isNotCore = true;

        internal void In(Expression target, params object[] inArguments)
            => _conditions.Add(new ConditionIn(IsNot, NextConnection, target, inArguments));

        internal void Like(Expression target, object serachText)
            => _conditions.Add(new ConditionLike(IsNot, NextConnection, target, serachText));

        internal void Between(Expression target, object min, object max)
            => _conditions.Add(new ConditionBetween(IsNot, NextConnection, target, min, max));
    }
}

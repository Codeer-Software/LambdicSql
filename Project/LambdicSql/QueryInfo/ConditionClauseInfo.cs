using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class ConditionClauseInfo
    {
        bool _isNotCore;
        ConditionConnection _nextConnectionCore;

        protected bool IsNot
        {
            get
            {
                var isNot = _isNotCore;
                _isNotCore = false;
                return isNot;
            }
        }

        protected ConditionConnection NextConnection
        {
            get
            {
                var nextConnectionCore = _nextConnectionCore;
                _nextConnectionCore = ConditionConnection.Non;
                return nextConnectionCore;
            }
        }

        public List<IConditionInfo> Conditions { get; } = new List<IConditionInfo>();

        public ConditionClauseInfo() { }

        public ConditionClauseInfo(Expression exp)
        {
            Conditions.Add(new ConditionInfoExpression(false, ConditionConnection.And, exp));
        }

        public ConditionClauseInfo Clone()
        {
            var dst = new ConditionClauseInfo();
            dst.Conditions.AddRange(Conditions);
            dst._isNotCore = _isNotCore;
            dst._nextConnectionCore = _nextConnectionCore;
            return dst;
        }

        internal void And(Expression exp)
            => Conditions.Add(new ConditionInfoExpression(IsNot, ConditionConnection.And, exp));

        internal void Or(Expression exp)
            => Conditions.Add(new ConditionInfoExpression(IsNot, ConditionConnection.Or, exp));

        internal void And()
            => _nextConnectionCore = ConditionConnection.And;

        internal void Or()
            => _nextConnectionCore = ConditionConnection.Or;

        internal void Not()
            => _isNotCore = true;

        internal void In<TTarget>(Expression target, TTarget[] inArguments)
            => Conditions.Add(new ConditionInfoIn(IsNot, NextConnection, target, inArguments.Cast<object>().ToList()));

        internal void Like(Expression target, string serachText)
            => Conditions.Add(new ConditionInfoLike(IsNot, NextConnection, target, serachText));

        internal void Between<TTarget>(Expression target, TTarget min, TTarget max)
            => Conditions.Add(new ConditionInfoBetween(IsNot, NextConnection, target, min, max));
    }
}

using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace LambdicSql.QueryInfo
{
    public class WhereInfo
    {
        bool _isNot;
        ConditionConnection _nextConnection;

        bool IsNot
        {
            get
            {
                var isNot = _isNot;
                _isNot = false;
                return isNot;
            }
        }

        public List<IConditionInfo> Conditions { get; } = new List<IConditionInfo>();

        public WhereInfo() { }

        public WhereInfo(Expression exp)
        {
            Conditions.Add(new ConditionInfoExpression(false, ConditionConnection.And, exp));
        }

        public WhereInfo Clone()
        {
            var clone = new WhereInfo();
            clone.Conditions.AddRange(Conditions);
            return clone;
        }

        internal void And(Expression exp)
            => Conditions.Add(new ConditionInfoExpression(IsNot, ConditionConnection.And, exp));

        internal void Or(Expression exp)
            =>Conditions.Add(new ConditionInfoExpression(IsNot, ConditionConnection.Or, exp));

        internal void And()
            => _nextConnection = ConditionConnection.And;

        internal void Or()
            => _nextConnection = ConditionConnection.Or;

        internal void Not()
            => _isNot = true;

        internal void In<TTarget>(Expression target, TTarget[] inArguments)
            => Conditions.Add(new ConditionInfoIn(IsNot, _nextConnection, target, inArguments.Cast<object>().ToList()));

        internal void Like(Expression target, string serachText)
            => Conditions.Add(new ConditionInfoLike(IsNot, _nextConnection, target, serachText));

        internal void Between<TTarget>(Expression target, TTarget min, TTarget max)
            => Conditions.Add(new ConditionInfoBetween(IsNot, _nextConnection, target, min, max));
    }
}

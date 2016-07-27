using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Clause.Condition
{
    public class ConditionClause
    {
        bool _isNotCore;
        ConditionConnection _nextConnectionCore;
        List<ICondition> _conditions = new List<ICondition>();
        ConditionClause _currentBlock;

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

        public bool HasBlock { get; private set; }

        public ICondition[] GetConditions() => _conditions.ToArray();

        protected ConditionClause() { }

        protected ConditionClause(Expression exp)
        {
            _conditions.Add(new ConditionExpression(false, ConditionConnection.And, exp));
        }

        protected ConditionClause(Expression exp, object parameters)
        {
            _conditions.Add(new ConditionExpression(false, ConditionConnection.And, exp, parameters));
        }
        
        protected ConditionClause Copy(ConditionClause dst)
        {
            dst._conditions.AddRange(_conditions);
            dst._isNotCore = _isNotCore;
            dst._nextConnectionCore = _nextConnectionCore;
            if (_currentBlock != null)
            {
                var block = new ConditionClause();
                dst._currentBlock = _currentBlock.Copy(block);
            }
            return dst;
        }

        internal void And(Expression exp)
        {
            if (_currentBlock == null) _conditions.Add(new ConditionExpression(IsNot, ConditionConnection.And, exp));
            else _currentBlock.And(exp);
        }

        internal void Or(Expression exp)
        {
            if (_currentBlock == null) _conditions.Add(new ConditionExpression(IsNot, ConditionConnection.Or, exp));
            else _currentBlock.Or(exp);
        }

        internal void And()
        {
            if (_currentBlock == null) _nextConnectionCore = ConditionConnection.And;
            else _currentBlock.And();
        }

        internal void Or()
        {
            if (_currentBlock == null) _nextConnectionCore = ConditionConnection.Or;
            else _currentBlock.Or();
        }

        internal void Skip()
        {
            if (_currentBlock == null) _nextConnectionCore = ConditionConnection.Skip;
            else _currentBlock.Skip();
        }

        internal void Not()
        {
            if (_currentBlock == null) _isNotCore = true;
            else _currentBlock.Not();
        }

        internal void In(Expression target, params object[] inArguments)
        {
            var connection = NextConnection;
            if (connection == ConditionConnection.Skip) { }
            else if (_currentBlock == null) _conditions.Add(new ConditionIn(IsNot, connection, target, inArguments));
            else _currentBlock.In(target, inArguments);
        }

        internal void Like(Expression target, object serachText)
        {
            var connection = NextConnection;
            if (connection == ConditionConnection.Skip) { }
            else if (_currentBlock == null) _conditions.Add(new ConditionLike(IsNot, connection, target, serachText));
            else _currentBlock.Like(target, serachText);
        }

        internal void Between(Expression target, object min, object max)
        {
            var connection = NextConnection;
            if (connection == ConditionConnection.Skip) { }
            else if (_currentBlock == null) _conditions.Add(new ConditionBetween(IsNot, connection, target, min, max));
            else _currentBlock.Between(target, min, max);
        }

        public void BlockStart()
        {
            if (_currentBlock == null)
            {
                _currentBlock = new ConditionClause();
            }
            else
            {
                _currentBlock.BlockStart();
            }
        }

        public void BlockEnd()
        {
            if (_currentBlock == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                if (_currentBlock._currentBlock != null)
                {
                    _currentBlock.BlockEnd();
                }
                else
                {
                    var connection = NextConnection;
                    if (connection != ConditionConnection.Skip)
                    {
                        _conditions.Add(new MultiCondition(_currentBlock._conditions) { IsNot = IsNot, ConditionConnection = connection });
                    }
                    _currentBlock = null;
                }
            }
        }

        protected string ToString(ISqlStringConverter decoder, string clause)
            => ConditionCount == 0 ?
                string.Empty :
                string.Join(Environment.NewLine + "\t", new[] { clause }.Concat(GetConditions().Select((e, i) => ToString(decoder, e, i))).ToArray());
        
        internal static string ToString(ISqlStringConverter decoder, ICondition condition, int index)
        {
            var connection = string.Empty;
            if (index != 0)
            {
                switch (condition.ConditionConnection)
                {
                    case ConditionConnection.And: connection = "AND "; break;
                    case ConditionConnection.Or: connection = "OR "; break;
                    default: throw new NotSupportedException();
                }
            }
            var not = condition.IsNot ? "NOT " : string.Empty;
            return connection + not + "(" + condition.ToString(decoder) + ")";
        }
    }
}

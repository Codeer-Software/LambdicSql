using System.Linq.Expressions;
using System.Linq;
using LambdicSql.QueryBase;

namespace LambdicSql.Clause.Condition
{
    public class ConditionIn : ICondition
    {
        object[] _arguments { get; }
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public Expression Target { get; }
        public object[] GetArguments() => _arguments.ToArray();

        public ConditionIn(bool isNot, ConditionConnection connection, Expression target, object[] arguments)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Target = target;
            _arguments = arguments;
        }

        public string ToString(IExpressionDecoder decoder)
            => decoder.ToString(Target) + " IN(" + decoder.MakeSqlArguments(GetArguments()) + ")";
    }
}

using System.Linq.Expressions;

namespace LambdicSql.Clause.Update
{
    public class SetElement
    {
        public Expression SelectData { get; }
        public object Value { get; }
        public SetElement(Expression selectData, object value)
        {
            SelectData = selectData;
            Value = value;
        }
    }
}

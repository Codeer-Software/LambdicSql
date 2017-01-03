using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{

    class SqlSyntaxConditionAttribute : SqlSyntaxConverterNewAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, NewExpression exp)
        {
            var obj = converter.ToObject(exp.Arguments[0]);
            return (bool)obj ? converter.Convert(exp.Arguments[1]) : (BuildingParts)string.Empty;
        }
    }
}

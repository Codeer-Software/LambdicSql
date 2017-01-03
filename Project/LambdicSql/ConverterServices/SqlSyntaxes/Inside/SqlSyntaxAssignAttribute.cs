using LambdicSql.BuilderServices.Parts;
using LambdicSql.BuilderServices.Parts.Inside;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{

    class SqlSyntaxAssignAttribute : SqlSyntaxConverterNewAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, NewExpression exp)
        {
            BuildingParts arg1 = converter.Convert(exp.Arguments[0]).Customize(new CustomizeColumnOnly());
            return new HParts(arg1, "=", converter.Convert(exp.Arguments[1])) { Separator = " " };
        }
    }
}

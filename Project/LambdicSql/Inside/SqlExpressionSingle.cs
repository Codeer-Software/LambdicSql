using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    //TODO こいつらいらんやろ
    class SqlExpressionSingle<TSelected> : SqlExpression<TSelected>
    {
        public override BuildingParts BuildingParts { get; }

        public SqlExpressionSingle(DbInfo dbInfo, Expression core)
        {
            var converter = new ExpressionConverter(dbInfo);
            if (core == null) BuildingParts = string.Empty;
            else BuildingParts = converter.Convert(core);
        }

        public SqlExpressionSingle(DbInfo dbInfo, BuildingParts core)
        {
            BuildingParts = core;
        }
    }
}

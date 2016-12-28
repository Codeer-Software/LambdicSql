using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside
{
    /// <summary>
    /// SQL Window functions.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// </summary>
    static class Window
    {

        internal static ExpressionElement ConvertRows(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var exp = methods[0];
            var args = exp.Arguments.Select(e => converter.Convert(e)).ToArray();

            //Sql server can't use parameter.
            if (exp.Arguments.Count == 1)
            {
                return LineSpace("ROWS", args[0].Customize(new CustomizeParameterToObject()), "PRECEDING");
            }
            else
            {
                return LineSpace("ROWS BETWEEN", args[0].Customize(new CustomizeParameterToObject()),
                    "PRECEDING AND", args[1].Customize(new CustomizeParameterToObject()), "FOLLOWING");
            }
        }


        internal static ExpressionElement ConvertPartitionBy(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var exp = methods[0];

            var partitionBy = new VText();
            partitionBy.Add("PARTITION BY");

            var elements = new VText() { Indent = 1, Separator = "," };
            var array = exp.Arguments[0] as NewArrayExpression;
            foreach (var e in array.Expressions.Select(e => converter.Convert(e)))
            {
                elements.Add(e);
            }
            partitionBy.Add(elements);

            return partitionBy;
        }

        internal static ExpressionElement MakeOver(IExpressionConverter converter, MethodCallExpression[] methods, VText v, int argCount)
        {
            v.Add("OVER(");
            v.AddRange(1, methods[0].Arguments.Skip(argCount).
                Where(e => !(e is ConstantExpression)). //Skip null.
                Select(e => converter.Convert(e)).ToArray());
            return v.ConcatToBack(")");
        }
    }
}

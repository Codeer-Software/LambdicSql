using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    [SqlSyntax]
    public class PartitionBy
    {
        public PartitionBy(params object[] targets) { InvalitContext.Throw("new " + nameof(PartitionBy)); }
        public static string NewToString(ISqlStringConverter converter, NewExpression exp)
        {
            var arg = exp.Arguments[0];
            var array = arg as NewArrayExpression;
            return Environment.NewLine + "\tPARTITION BY" + Environment.NewLine + "\t\t" + 
                string.Join("," + Environment.NewLine + "\t\t", array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }
    }

    [SqlSyntax]
    public class OrderBy
    {
        public OrderBy(params IOrderElement[] elements) { InvalitContext.Throw("new " + nameof(OrderBy)); }
        public static string NewToString(ISqlStringConverter converter, NewExpression exp)
        {
            var arg = exp.Arguments[0];
            var array = arg as NewArrayExpression;
            return Environment.NewLine + "\tORDER BY" + Environment.NewLine + "\t\t" + 
                string.Join("," + Environment.NewLine + "\t\t", array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }
    }

    [SqlSyntax]
    public class Rows
    {
        public Rows(int p) { InvalitContext.Throw("new " + nameof(Rows)); }
        public Rows(int p1, int p2) { InvalitContext.Throw("new " + nameof(Rows)); }
        public static string NewToString(ISqlStringConverter converter, NewExpression exp)
        {
            var args = exp.Arguments.Select(e => converter.ToString(e)).ToArray();
            if (exp.Arguments.Count == 1)
            {
                return Environment.NewLine + "\tROWS " + args[0] + " PRECEDING";
            }
            else
            {
                return Environment.NewLine + "\tROWS BETWEEN " + converter.Context.Parameters.ResolvePrepare(args[0]) +
                    " PRECEDING AND " + converter.Context.Parameters.ResolvePrepare(args[1]) + " FOLLOWING";
            }
        }
    }

    [SqlSyntax]
    public static class Window
    {
        public interface IFuncAfter : IMethodChainGroup { }

        public static IFuncAfter Avg<T>(T t) => InvalitContext.Throw<IFuncAfter>(nameof(Avg));
        public static IFuncAfter Lag<T>(T t) => InvalitContext.Throw<IFuncAfter>(nameof(Lag));
        public static IFuncAfter Lag<T>(T t, object offset) => InvalitContext.Throw<IFuncAfter>(nameof(Lag));
        public static IFuncAfter Lag<T>(T t, object offset, object @default) => InvalitContext.Throw<IFuncAfter>(nameof(Lag));
        public static T Over<T>(this IFuncAfter words, PartitionBy p, OrderBy o, Rows r) => InvalitContext.Throw<T>(nameof(Over));

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var func = converter.MakeNormalSqlFunctionString(methods[0]);

            var overMethod = methods[1];
            var over = overMethod.Method.Name.ToUpper() + "(" +
                string.Join(" ", overMethod.Arguments.Skip(1).Where(e=>!(e is ConstantExpression)).Select(e => converter.ToString(e)).ToArray()) + ")";

            return func + over;
        }
    }
}

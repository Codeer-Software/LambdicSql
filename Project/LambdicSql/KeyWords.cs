using LambdicSql.Inside;
using LambdicSql.Inside.Keywords;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    [SqlSyntax]
    public class Assign
    {
        protected Assign() { }
        public Assign(object target, object value) { InvalitContext.Throw("new " + nameof(Assign)); }
        public static string NewToString(ISqlStringConverter converter, NewExpression exp)
        {
            var args = exp.Arguments.Select(e => converter.ToString(e)).ToArray();
            return args[0] + " = " + args[1];
        }
    }

    [SqlSyntax]
    public class AssignWithType : Assign
    {
        protected AssignWithType() { }
        public AssignWithType(object target, object value) { InvalitContext.Throw("new " + nameof(Assign)); }
        public new static string NewToString(ISqlStringConverter converter, NewExpression exp)
        {
            var col = converter.ToString(exp.Arguments[0]);
            var val = converter.ToString(exp.Arguments[1]);

            var param = converter.Context.DbInfo.GetDbParamByLambdaName(col);
            if (param != null)
            {
                converter.Context.Parameters.SetDbParam(val, param);
            }
            return col + " = " + val;
        }
    }
    //Set operation
    [SqlSyntax]
    public static class Keywords
    {
        public static IQuery<TSelected> Select<TSelected>(AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));
        public static IQuery<TSelected> Select<TSelected>(TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));
        public static IQuery<TSelected> SelectFrom<TSelected>(TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(SelectFrom));
        public static IQuery<TSelected> Select<TSelected>(this IQuery words, AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));
        public static IQuery<TSelected> Select<TSelected>(this IQuery words, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));
        public static IQuery<TSelected> SelectFrom<TSelected>(this IQuery words, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(SelectFrom));

        public interface ICaseAfter : IQueryGroup { }
        public interface IWhenAfter : IQueryGroup { }
        public interface IWhenAfter<T> : IQueryGroup { }
        public interface IThenAfter<T> : IQueryGroup { }
        public interface IElseAfter<T> : IQueryGroup { }
        public static ICaseAfter Case() => InvalitContext.Throw<ICaseAfter>(nameof(Case));
        public static ICaseAfter Case<T>(T t) => InvalitContext.Throw<ICaseAfter>(nameof(Case));
        public static IWhenAfter When<T>(this ICaseAfter words, T t) => InvalitContext.Throw<IWhenAfter>(nameof(When));
        public static IWhenAfter<T1> When<T1, T2>(this IThenAfter<T1> words, T2 t) => InvalitContext.Throw<IWhenAfter<T1>>(nameof(When));
        public static IThenAfter<T> Then<T>(this IWhenAfter words, T t) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));
        public static IThenAfter<T> Then<T>(this IWhenAfter<T> words, T t) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));
        public static IElseAfter<T> Else<T>(this IThenAfter<T> words, T t) => InvalitContext.Throw<IElseAfter<T>>(nameof(Then));
        public static T End<T>(this IThenAfter<T> words) => InvalitContext.Throw<T>(nameof(End));
        public static T End<T>(this IElseAfter<T> words) => InvalitContext.Throw<T>(nameof(End));

        public static IQuery<Non> Delete() => InvalitContext.Throw<IQuery<Non>>(nameof(Delete));

        public interface IFromAfter<T> : IQueryGroup<T> { }
        public static IFromAfter<TSelected> From<TSelected>(this IQuery<TSelected> words, params object[] tbale) => InvalitContext.Throw<IFromAfter<TSelected>>(nameof(From));
        public static IFromAfter<Non> From(params object[] tbale) => InvalitContext.Throw<IFromAfter<Non>>(nameof(From));
        public static IFromAfter<TSelected> Join<TSelected, T>(this IFromAfter<TSelected> words, T tbale, bool condition) => InvalitContext.Throw<IFromAfter<TSelected>>(nameof(Join));
        public static IFromAfter<TSelected> LeftJoin<TSelected, T>(this IFromAfter<TSelected> words, T tbale, bool condition) => InvalitContext.Throw<IFromAfter<TSelected>>(nameof(LeftJoin));
        public static IFromAfter<TSelected> RightJoin<TSelected, T>(this IFromAfter<TSelected> words, T tbale, bool condition) => InvalitContext.Throw<IFromAfter<TSelected>>(nameof(RightJoin));
        public static IFromAfter<TSelected> CrossJoin<TSelected, T>(this IFromAfter<TSelected> words, T tbale) => InvalitContext.Throw<IFromAfter<TSelected>>(nameof(CrossJoin));

        public static IQuery<Non> GroupBy(params object[] target) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupBy));
        public static IQuery<TSelected> GroupBy<TSelected>(this IQuery<TSelected> words, params object[] target) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupBy));
        public static IQuery<Non> GroupByRollup(params object[] target) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupByRollup));
        public static IQuery<TSelected> GroupByRollup<TSelected>(this IQuery<TSelected> words, params object[] target) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupByRollup));
        public static IQuery<Non> GroupByCube(params object[] target) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupByCube));
        public static IQuery<TSelected> GroupByCube<TSelected>(this IQuery<TSelected> words, params object[] target) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupByCube));
        public static IQuery<Non> GroupByGroupingSets(params object[] target) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupByGroupingSets));
        public static IQuery<TSelected> GroupByGroupingSets<TSelected>(this IQuery<TSelected> words, params object[] target) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupByGroupingSets));

        public static IQuery<Non> Having(bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(Having));
        public static IQuery<TSelected> Having<TSelected>(this IQuery<TSelected> words, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Having));

        public interface IInsertIntoAfter<TSelected, TTable> : IQueryGroup<TSelected> { }
        public static IInsertIntoAfter<Non, TTable> InsertInto<TTable>(TTable table, params object[] targets) => InvalitContext.Throw<IInsertIntoAfter<Non, TTable>>(nameof(InsertInto));
        public static IInsertIntoAfter<Non, TTable> InsertIntoAll<TTable>(TTable table) => InvalitContext.Throw<IInsertIntoAfter<Non, TTable>>(nameof(InsertInto));
        public static IInsertIntoAfter<Non, TTable> InsertIntoIgnoreDbGenerated<TTable>(TTable table) => InvalitContext.Throw<IInsertIntoAfter<Non, TTable>>(nameof(InsertInto));
        public static IQuery<TSelected> Values<TSelected, TTable>(this IInsertIntoAfter<TSelected, TTable> words, params object[] targets)
             => InvalitContext.Throw<IQuery<TSelected>>(nameof(Values));
        public static IQuery<TSelected> Values<TSelected, TTable>(this IInsertIntoAfter<TSelected, TTable> words, TTable value)
             => InvalitContext.Throw<IQuery<TSelected>>(nameof(Values));
        public static IQuery<TSelected> ValuesWithTypes<TSelected, TTable>(this IInsertIntoAfter<TSelected, TTable> words, TTable value)
            => InvalitContext.Throw<IQuery<TSelected>>(nameof(Values));

        public interface IOrderByAfter<T> : IQueryGroup<T> { }
        public static IOrderByAfter<Non> OrderBy(params IOrderElement[] elements) => InvalitContext.Throw<IOrderByAfter<Non>>(nameof(OrderBy));
        public static IOrderByAfter<TSelected> OrderBy<TSelected>(this IQuery<TSelected> words, params IOrderElement[] elements) => InvalitContext.Throw<IOrderByAfter<TSelected>>(nameof(OrderBy));

        public interface IUpdateAfter<TSelected, T> : IQueryGroup<TSelected> { }
        public static IUpdateAfter<Non, T> Update<T>(T table) => InvalitContext.Throw<IUpdateAfter<Non, T>>(nameof(Update));
        public static IQuery<TSelected> Set<TSelected, T>(this IUpdateAfter<TSelected, T> words, params Assign[] assigns) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Set));

        public static IQuery<Non> Where(bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(Where));
        public static IQuery<TSelected> Where<TSelected>(this IQuery<TSelected> words, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Where));

        public static bool Like(string target, string serachText) => InvalitContext.Throw<bool>(nameof(Like));
        public static bool Between<TTarget>(TTarget target, TTarget min, TTarget max) => InvalitContext.Throw<bool>(nameof(Between));
        public static bool In<TTarget>(TTarget target, params TTarget[] inArguments) => InvalitContext.Throw<bool>(nameof(In));
        public static bool In(ISqlExpression exp) => InvalitContext.Throw<bool>(nameof(In));
        public static bool In(IQuery exp) => InvalitContext.Throw<bool>(nameof(In));
        public static bool Exists(ISqlExpression exp) => InvalitContext.Throw<bool>(nameof(Exists));
        public static bool Exists(IQuery exp) => InvalitContext.Throw<bool>(nameof(Exists));

        public static IQuery<Non> Union(bool isAll = false) => InvalitContext.Throw<IQuery<Non>>(nameof(Union));
        public static IQuery<TSelected> Union<TSelected>(this IQuery<TSelected> words, bool isAll = false) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Union));
        public static IQuery<Non> Intersect(bool isAll = false) => InvalitContext.Throw<IQuery<Non>>(nameof(Intersect));
        public static IQuery<TSelected> Intersect<TSelected>(this IQuery<TSelected> words, bool isAll = false) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Intersect));
        public static IQuery<Non> Except(bool isAll = false) => InvalitContext.Throw<IQuery<Non>>(nameof(Except));
        public static IQuery<TSelected> Except<TSelected>(this IQuery<TSelected> words, bool isAll = false) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Except));
        public static IQuery<Non> Minus(bool isAll = false) => InvalitContext.Throw<IQuery<Non>>(nameof(Minus));
        public static IQuery<TSelected> Minus<TSelected>(this IQuery<TSelected> words, bool isAll = false) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Minus));

        static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(Select):
                case nameof(SelectFrom):
                    return SelectClause.ToString(converter, methods);
                case nameof(Case):
                    return CaseClause.ToString(converter, methods);
                case nameof(Delete):
                    return DeleteClause.ToString(converter, methods);
                case nameof(From):
                    return FromClause.ToString(converter, methods);
                case nameof(GroupBy):
                    return GroupByClause.ToString(converter, methods);
                case nameof(Having):
                    return HavingClause.ToString(converter, methods);
                case nameof(InsertInto):
                case nameof(InsertIntoAll):
                case nameof(InsertIntoIgnoreDbGenerated):
                    return InsertIntoClause.ToString(converter, methods);
                case nameof(OrderBy):
                    return OrderByWordsClause.ToString(converter, methods);
                case nameof(Update):
                    return UpdateClause.ToString(converter, methods);
                case nameof(Where):
                    return WhereClause.ToString(converter, methods);
                case nameof(In):
                case nameof(Like):
                case nameof(Between):
                    return ConditionKeyWords.ToString(converter, methods);
            }
            throw new NotSupportedException();
        }
    }
}

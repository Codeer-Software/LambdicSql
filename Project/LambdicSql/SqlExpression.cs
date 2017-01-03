using LambdicSql.ConverterServices;
using LambdicSql.Inside;
using LambdicSql.BuilderServices.Parts;

namespace LambdicSql
{
    //こいつの役割はBuildingPartsの複雑さをラッピングして消しているだけ
    //Expressionという抽象的な名前のせいで役割が分からなくなっているが
    //基本はSqlPartsでそれに型Tを持っているだけとうのが正しい
    //だから統合してもいいくらいなんだけど・・・

    //とは言えこれでもいいといえばいい。
    //BuildingPartsのリファクタリング次第できめるしかないか・・・

    /// <summary>
    /// Expression.
    /// </summary>
    public interface ISqlExpression
    {
        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        BuildingParts BuildingParts { get; }
    }

    //TODO 抽象である必要がない
    /// <summary>
    /// Expressions that represent part of the query.
    /// </summary>
    /// <typeparam name="T">The type represented by SqlExpression.</typeparam>
    public abstract class SqlExpression<T> : ISqlExpression
    {
        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        public abstract BuildingParts BuildingParts { get; }

        /// <summary>
        /// Entity represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        public T Body => InvalitContext.Throw<T>(nameof(Body));

        /// <summary>
        /// Implicitly convert to the type represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator T(SqlExpression<T> src) => InvalitContext.Throw<T>("implicit operator");
    }
}

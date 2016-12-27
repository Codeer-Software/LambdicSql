using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;

namespace LambdicSql
{
    //TODO このレイヤはなくしてしまう方がいいなー
    //でないと、implicitの機能が失われてしまう場合がある
    /// <summary>
    /// Expression.
    /// </summary>
    public interface ISqlExpression
    {
        /// <summary>
        /// Data Base info.
        /// </summary>
        DbInfo DbInfo { get; }

        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        SqlText SqlText { get; }
    }

    /// <summary>
    /// Expression.
    /// </summary>
    /// <typeparam name="TReturn"></typeparam>
    public interface ISqlExpression<out TReturn> : ISqlExpression
    {

        /// <summary>
        /// The type that the expression represents.
        /// </summary>
        TReturn Body { get; }
    }
}

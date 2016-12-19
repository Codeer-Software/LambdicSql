using LambdicSql.SqlBase;

namespace LambdicSql
{
    /// <summary>
    /// TOP keyword.
    /// </summary>
    public interface ITop { }

    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    public interface IAsterisk { }

    /// <summary>
    /// It's *.
    /// Used in Select clause and Count function.
    /// </summary>
    /// <typeparam name="T">It represents the type to select when used in the Select clause.</typeparam>
    public interface IAsterisk<T> : IAsterisk { }

    /// <summary>
    /// It is an object representing the sort order
    /// Implemented classes include Asc and Desc.
    /// </summary>
    public interface ISortedBy { }

    /// <summary>
    /// ORDERBY keyword.
    /// Use it with the OVER function.
    /// </summary>
    public interface IOrderBy : IClauseChain<Non> { }

    /// <summary>
    /// PARTITION BY keyword.
    /// Use it with the OVER function.
    /// </summary>
    public interface IPartitionBy { }

    /// <summary>
    /// Rows keyword.
    /// Use it with the OVER function.
    /// </summary>
    public interface IRows { }

    /// <summary>
    /// Non.
    /// This is entered if there is no type selected in the SELECT clause.
    /// </summary>
    public class Non
    {
        private Non() { }
    }
}

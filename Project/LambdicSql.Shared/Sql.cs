using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.ConverterServices;
using System;

namespace LambdicSql
{
    /// <summary>
    /// Sql.
    /// </summary>
    public class Sql
    {
        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        public ICode Code { get; }

        internal Sql(ICode code)
        {
            Code = code;
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql operator + (Sql sql1, Sql sql2)
            => new Sql(new VCode(sql1.Code, sql2.Code));

        //TODO test.
        /// <summary>
        /// Cast.
        /// </summary>
        /// <typeparam name="TDst">Destination type.</typeparam>
        /// <returns>Casted result.</returns>
        public Sql<TDst> Cast<TDst>() => new Sql<TDst>(Code);
    }

    /// <summary>
    /// Sql.
    /// </summary>
    /// <typeparam name="T">The type represented by sql.</typeparam>
    public class Sql<T> : Sql
    {
        /// <summary>
        /// Entity represented by sql.
        /// It can only be used within methods of the LambdicSql.Db class.
        /// </summary>
        public T Body { get { throw new InvalitContextException(nameof(Body)); } }

        /// <summary>
        /// Implicitly convert to the type represented by sql.
        /// It can only be used within methods of the LambdicSql.Db class.
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator T(Sql<T> src) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Concatenate sql1 and sql2.
        /// </summary>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<T> operator + (Sql<T> sql1, Sql sql2)
          => new Sql<T>(new VCode(sql1.Code, sql2.Code));

        internal Sql(ICode code) : base(code) { }
    }

    /// <summary>
    /// Represent arguments of recursive SQL.
    /// </summary>
    /// <typeparam name="TSelected">The type represented by Sql.</typeparam>
    public class SqlRecursiveArguments<TSelected> : Sql<TSelected>
    {
        internal SqlRecursiveArguments(ICode code) : base(code) { }
    }
}

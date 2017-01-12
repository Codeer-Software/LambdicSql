using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.CodeParts;
using System;
using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;

namespace LambdicSql
{
    /// <summary>
    /// Expression.
    /// </summary>
    public class Sql
    {
        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        public Code Code { get; }

        internal Sql(Code code)
        {
            Code = code;
        }

        /// <summary>
        /// Concatenate expression1 and expression2.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql operator + (Sql expression1, Sql expression2)
          => new Sql(new VCode(expression1.Code, expression2.Code));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public BuildedSql Build(Type connectionType)
            => Build(DialectResolver.CreateCustomizer(connectionType.FullName));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>Sql information.</returns>
        public BuildedSql Build(DialectOption option)
        {
            var context = new BuildingContext(option);
            return new BuildedSql(Code.ToString(true, 0, context), context.ParameterInfo.GetDbParams());
        }

        //TODO test.
        /// <summary>
        /// Cast.
        /// </summary>
        /// <typeparam name="TDst">Destination type.</typeparam>
        /// <returns>Casted result.</returns>
        public Sql<TDst> Cast<TDst>() => new Sql<TDst>(Code);
    }

    /// <summary>
    /// Expressions that represent part of the query.
    /// </summary>
    /// <typeparam name="T">The type represented by SqlExpression.</typeparam>
    public class Sql<T> : Sql
    {
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
        public static implicit operator T(Sql<T> src) => InvalitContext.Throw<T>("implicit operator");

        /// <summary>
        /// Concatenate expression1 and expression2.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<T> operator + (Sql<T> expression1, Sql expression2)
          => new Sql<T>(new VCode(expression1.Code, expression2.Code));

        /// <summary>
        /// Sql information.
        /// This have static information of the type selected in the SELECT clause.
        /// </summary>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public new BuildedSql<T> Build(Type connectionType)
          => new BuildedSql<T>(base.Build(connectionType));

        /// <summary>
        /// Sql information.
        /// This have static information of the type selected in the SELECT clause.
        /// </summary>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>Sql information.</returns>
        public new BuildedSql<T> Build(DialectOption option)
          => new BuildedSql<T>(base.Build(option));

        internal Sql(Code code) : base(code) { }
    }

    /// <summary>
    /// Expressions that represent arguments of recursive SQL.
    /// </summary>
    /// <typeparam name="TSelected">The type represented by SqlExpression.</typeparam>
    public class SqlRecursiveArguments<TSelected> : Sql<TSelected>
    {
        internal SqlRecursiveArguments(Code code) : base(code) { }
    }
}

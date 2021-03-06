﻿using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.ConverterServices;
using System;
using System.Collections.Generic;

namespace LambdicSql
{
    /// <summary>
    /// Sql.
    /// </summary>
    public class Sql : SqlExpression
    {
        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        public ICode Code { get; }

        /// <summary>
        /// Is empty.
        /// </summary>
        public bool IsEmpty => Code.IsEmpty;

        internal Sql(ICode code)
        {
            Code = code;
        }

        /// <summary>
        /// Empty Constructor.
        /// </summary>
        public Sql()
        {
            Code = string.Empty.ToCode();
        }

        /// <summary>
        /// Addition operator.
        /// </summary>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql operator +(Sql sql1, Sql sql2) => new Sql(ExpressionConverter.AddCode(sql1.Code, sql2.Code));

        /// <summary>
        /// Addition operator.
        /// It can only be used within lambda of the LambdicSql.
        /// </summary>
        /// <param name="sql">sql.</param>
        /// <param name="exp">expression.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql operator +(Sql sql, SqlExpression exp) { throw new InvalitContextException("addition operator"); }

        //TODO Make it possible to decide based on the type of defined phrase class.
        /// <summary>
        /// Build.
        /// </summary>
        /// <param name="connectionType">Connection's type.</param>
        /// <returns>SQL text and parameters.</returns>
        public BuildedSql Build(Type connectionType)
            => Build(DialectResolver.CreateCustomizer(connectionType.FullName));

        /// <summary>
        /// Build.
        /// </summary>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>SQL text and parameters.</returns>
        public BuildedSql Build(DialectOption option)
        {
            var context = new BuildingContext(option);
            var sqalText = Code.ToString(context);
            return new BuildedSql(sqalText, context.ParameterInfo.GetDbParams());
        }

        /// <summary>
        /// Cast.
        /// </summary>
        /// <typeparam name="TDst">Destination type.</typeparam>
        /// <returns>Casted result.</returns>
        public Sql<TDst> Cast<TDst>() => new Sql<TDst>(Code);

        /// <summary>
        /// Change parameters.
        /// </summary>
        /// <param name="values">New values.</param>
        /// <returns>BuildingSql after change.</returns>
        public Sql ChangeParams(Dictionary<string, object> values)
            => new Sql(Code.Accept(new CustomizeParameterValue(values)));
    }

    /// <summary>
    /// Sql.
    /// </summary>
    /// <typeparam name="T">The type represented by sql.</typeparam>
    public class Sql<T> : Sql
    {
        /// <summary>
        /// Entity represented by sql.
        /// It can only be used within lambda of the LambdicSql.
        /// </summary>
        public T Body { get { throw new InvalitContextException(nameof(Body)); } }

        /// <summary>
        /// Variable name.
        /// It can only be used within lambda of the LambdicSql.
        /// </summary>
        public string Name => throw new InvalitContextException(nameof(Name));

        /// <summary>
        /// Implicitly convert to the type represented by sql.
        /// It can only be used within lambda of the LambdicSql.
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator T(Sql<T> src) { throw new InvalitContextException("implicit operator"); }

        /// <summary>
        /// Concatenate sql1 and sql2.
        /// </summary>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<T> operator +(Sql<T> sql1, Sql sql2) => new Sql<T>(ExpressionConverter.AddCode(sql1.Code, sql2.Code));

        /// <summary>
        /// Addition operator.
        /// It can only be used within lambda of the LambdicSql.
        /// </summary>
        /// <param name="sql">sql.</param>
        /// <param name="exp">expression.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<T> operator +(Sql<T> sql, SqlExpression exp) { throw new InvalitContextException("addition operator"); }

        /// <summary>
        /// Build.
        /// This have static information of the type selected in the SELECT clause.
        /// </summary>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>SQL text and parameters.</returns>
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

        /// <summary>
        /// Change parameters.
        /// </summary>
        /// <param name="values">New values.</param>
        /// <returns>BuildingSql after change.</returns>
        public new Sql<T> ChangeParams(Dictionary<string, object> values)
            => new Sql<T>(Code.Accept(new CustomizeParameterValue(values)));

        /// <summary>
        /// Empty Constructor.
        /// </summary>
        public Sql() { }

        internal Sql(ICode code) : base(code) { }
    }

    /// <summary>
    /// Represent arguments of recursive SQL.
    /// </summary>
    /// <typeparam name="TSelected">The type represented by Sql.</typeparam>
    public class SqlRecursiveArguments<TSelected> : Sql<TSelected>
    {
        internal SqlRecursiveArguments(ICode code) : base(code) { }

        /// <summary>
        /// Change parameters.
        /// </summary>
        /// <param name="values">New values.</param>
        /// <returns>BuildingSql after change.</returns>
        public new SqlRecursiveArguments<TSelected> ChangeParams(Dictionary<string, object> values)
            => new SqlRecursiveArguments<TSelected>(Code.Accept(new CustomizeParameterValue(values)));
    }
}
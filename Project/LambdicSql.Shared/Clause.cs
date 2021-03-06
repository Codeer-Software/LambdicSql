﻿using LambdicSql.ConverterServices;

namespace LambdicSql
{
    /// <summary>
    /// Clause.
    /// </summary>
    public abstract class Clause : SqlExpression
    {
        /// <summary>
        /// Addition operator.
        /// </summary>
        /// <param name="clause">clause.</param>
        /// <param name="exp">expression.</param>
        /// <returns>Concatenated result.</returns>
        public static Clause operator +(Clause clause, SqlExpression exp) { throw new InvalitContextException("addition operator"); }
    }

    /// <summary>
    /// Clause.
    /// </summary>
    /// <typeparam name="TSelected">The type represented by clause.</typeparam>
    public abstract class Clause<TSelected> : Clause
    {
        /// <summary>
        /// Addition operator.
        /// </summary>
        /// <param name="clause">clause.</param>
        /// <param name="exp">expression.</param>
        /// <returns>Concatenated result.</returns>
        public static Clause<TSelected> operator +(Clause<TSelected> clause, SqlExpression exp) { throw new InvalitContextException("addition operator"); }
        
        /// <summary>
        /// Implicitly convert to the type represented by Clause.
        /// </summary>
        /// <param name="src">Clause chain.</param>
        public static implicit operator TSelected(Clause<TSelected> src) { throw new InvalitContextException("implicit operator"); }
    }
}

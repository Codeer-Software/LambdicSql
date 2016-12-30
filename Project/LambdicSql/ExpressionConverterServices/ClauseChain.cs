﻿using LambdicSql.Inside;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Query.
    /// </summary>
    public interface IClauseChain { }
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSelected"></typeparam>
    public class ClauseChain<TSelected> : IClauseChain
    {
        /// <summary>
        /// Entity represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        public TSelected Body { get; }

        internal ClauseChain() { }

        /// <summary>
        /// Implicitly convert to the type represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator TSelected(ClauseChain<TSelected> src) => InvalitContext.Throw<TSelected>("implicit operator");
    }
}

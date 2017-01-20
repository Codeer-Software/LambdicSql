﻿namespace LambdicSql.ConverterServices
{
    /// <summary>
    /// Represents a parameter information.
    /// </summary>
    public interface IDbParam
    {
        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        object Value { get; set; }
        //TODO setはよくないな
    }
}

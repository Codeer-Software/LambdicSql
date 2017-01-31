namespace LambdicSql
{
    /// <summary>
    /// Represents a parameter information.
    /// </summary>
    public interface IDbParam
    {
        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        object Value { get; }

        /// <summary>
        /// Change Value.
        /// </summary>
        /// <param name="value">New Value.</param>
        /// <returns>IDbParam after change.</returns>
        IDbParam ChangeValue(object value);
    }
}

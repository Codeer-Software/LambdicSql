﻿namespace LambdicSql.BuilderServices.Syntaxes
{
    /// <summary>
    /// Customizer.
    /// </summary>
    public interface ISyntaxCustomizer
    {
        /// <summary>
        /// Coustom.
        /// </summary>
        /// <param name="src">Source.</param>
        /// <returns>Result.</returns>
        TextPartsBase Custom(TextPartsBase src);
    }
}

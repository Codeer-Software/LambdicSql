using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices.Inside.CodeParts;

namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// Change only column name without table name.
    /// </summary>
    public class CustomizeColumnOnly : ICodeCustomizer
    {
        /// <summary>
        /// Visit and customize.
        /// </summary>
        /// <param name="src">Source.</param>
        /// <returns>Destination.</returns>
        public ICode Visit(ICode src)
        {
            var col = src as DbColumnCode;
            return col == null ? src : col.ToColumnOnly();
        }
    }
}

using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for keyword.
    /// </summary>
    public class ObjectToStringConverterAttribute : ObjectConverterAttribute
    {
        /// <summary>
        /// Convert object to code.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Parts.</returns>
        public override Code Convert(object obj)
            => obj == null ? string.Empty : obj.ToString().ToUpper();
    }
}

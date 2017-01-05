using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for keyword.
    /// </summary>
    public class KeywordObjectConverterAttribute : SymbolConverterObjectAttribute
    {
        /// <summary>
        /// Name.If it is empty, use the ToString of the object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert object to building parts.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>BuildingParts.</returns>
        public override BuildingParts Convert(object obj)
            => obj == null ? string.Empty :
               string.IsNullOrEmpty(Name) ? obj.ToString().ToUpper() : Name;
    }
}

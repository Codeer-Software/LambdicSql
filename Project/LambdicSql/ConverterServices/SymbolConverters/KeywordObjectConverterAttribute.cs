using LambdicSql.BuilderServices.Syntaxes;

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
        /// Convert object to syntax.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Syntax.</returns>
        public override Syntax Convert(object obj)
            => obj == null ? string.Empty :
               string.IsNullOrEmpty(Name) ? obj.ToString().ToUpper() : Name;
    }
}

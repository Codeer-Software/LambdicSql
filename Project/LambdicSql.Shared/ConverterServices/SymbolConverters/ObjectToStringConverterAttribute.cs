using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.BuilderServices.Inside;

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
        public override ICode Convert(object obj)
            => (obj == null ? string.Empty : obj.ToString().ToUpper()).ToCode();
    }
}

using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices.Inside.CodeParts;

namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// Change parameters to embed directly in SQL.
    /// </summary>
    public class CustomizeParameterToObject : ICodeCustomizer
    {
        /// <summary>
        /// Visit and customize.
        /// </summary>
        /// <param name="src">Source.</param>
        /// <returns>Destination.</returns>
        public ICode Visit(ICode src)
        {
            var param = src as ParameterCode;
            return param == null ? src : param.ToDisplayValue();
        }
    }
}

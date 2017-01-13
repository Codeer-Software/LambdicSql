using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices.Inside.CodeParts;

namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomizeParameterToObject : ICodeCustomizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public Code Custom(Code src)
        {
            var param = src as ParameterCode;
            return param == null ? src : param.ToDisplayValue();
        }
    }
}

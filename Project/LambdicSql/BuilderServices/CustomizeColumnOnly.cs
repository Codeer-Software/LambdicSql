using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices.Inside.CodeParts;

namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomizeColumnOnly : ICodeCustomizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public Code Custom(Code src)
        {
            var col = src as DbColumnCode;
            return col == null ? src : col.ToColumnOnly();
        }
    }
}

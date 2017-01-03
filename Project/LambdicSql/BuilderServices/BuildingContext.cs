using LambdicSql.ConverterServices;
using System.Collections.Generic;

namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// Context of SQL conversion.
    /// </summary>
    public class BuildingContext
    {
        /// <summary>
        /// Option
        /// </summary>
        public DialectOption Option { get; }

        /// <summary>
        /// Parameter info.
        /// </summary>
        public ParameterInfo ParameterInfo { get; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, bool> WithEntied { get; } = new Dictionary<string, bool>();

        internal BuildingContext(DialectOption option)
        {
            Option = option;
            ParameterInfo = new ParameterInfo(option.ParameterPrefix);
        }
    }
}

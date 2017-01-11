using System.Collections.Generic;

namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// Context of SQL building.
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

        //TODO このあたり、一般にも使えそうなデザインにする
        /// <summary>
        /// Name of subquery entied in With clause.
        /// </summary>
        public Dictionary<string, bool> WithEntied { get; } = new Dictionary<string, bool>();

        internal BuildingContext(DialectOption option)
        {
            Option = option;
            ParameterInfo = new ParameterInfo(option.ParameterPrefix);
        }
    }
}

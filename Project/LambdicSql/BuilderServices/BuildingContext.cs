using System;
using System.Collections.Generic;

namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// Context of SQL building.
    /// </summary>
    public class BuildingContext
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsTopLevelQuery { get; } = true;

        /// <summary>
        /// 
        /// </summary>
        public int Indent { get; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indent"></param>
        /// <returns></returns>
        public BuildingContext ChangeIndent(int indent) => new BuildingContext(IsTopLevelQuery, indent, Option, ParameterInfo, WithEntied);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BuildingContext ChangeTopLevelQuery(bool isTopLevelQuery) => new BuildingContext(isTopLevelQuery, Indent, Option, ParameterInfo, WithEntied);
        

        BuildingContext(bool isTopLevel, int indent, DialectOption option, ParameterInfo parameterInfo, Dictionary<string, bool> withEntied)
        {
            IsTopLevelQuery = isTopLevel;
            Indent = indent;
            Option = option;
            ParameterInfo = parameterInfo;
            WithEntied = withEntied;
        }
    }
}

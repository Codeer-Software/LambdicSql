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
        /// Is it a top-level query?
        /// </summary>
        public bool IsTopLevelQuery { get; } = true;

        /// <summary>
        /// Indent.
        /// </summary>
        public int Indent { get; }

        /// <summary>
        /// Options for resolving dialects per database.
        /// </summary>
        public DialectOption DialectOption { get; }

        /// <summary>
        /// Parameter info.
        /// </summary>
        public ParameterInfo ParameterInfo { get; }

        /// <summary>
        /// Data that users set up and use when building SQL.
        /// </summary>
        public Dictionary<Type, object> UserData { get; } = new Dictionary<Type, object>();

        /// <summary>
        /// Generate clone with indent changed.
        /// </summary>
        /// <param name="indent"></param>
        /// <returns></returns>
        public BuildingContext ChangeIndent(int indent) => new BuildingContext(IsTopLevelQuery, indent, DialectOption, ParameterInfo, UserData);

        /// <summary>
        /// Generate clone with changed top level flag.
        /// </summary>
        /// <returns></returns>
        public BuildingContext ChangeTopLevelQuery(bool isTopLevelQuery) => new BuildingContext(isTopLevelQuery, Indent, DialectOption, ParameterInfo, UserData);

        internal BuildingContext(DialectOption dialectOption)
        {
            DialectOption = dialectOption;
            ParameterInfo = new ParameterInfo(dialectOption.ParameterPrefix);
        }

        BuildingContext(bool isTopLevel, int indent, DialectOption dialectOption, ParameterInfo parameterInfo, Dictionary<Type, object> userData)
        {
            IsTopLevelQuery = isTopLevel;
            Indent = indent;
            DialectOption = dialectOption;
            ParameterInfo = parameterInfo;
            UserData = userData;
        }
    }
}

using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.ConverterServices.Inside.CodeParts;

namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// Change parameters to define name in SQL.
    /// </summary>
    public class CustomizeParameterToVariableName : ICodeCustomizer
    {
        class VariableNameCode : ICode
        {
            string _name;

            internal VariableNameCode(string name)
            {
                _name = name;
            }

            public bool IsEmpty { get; }
            public bool IsSingleLine(BuildingContext context) => true;
            public string ToString(BuildingContext context) => PartsUtils.GetIndent(context.Indent) + _name;
            public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);
        }

        /// <summary>
        /// Visit and customize.
        /// </summary>
        /// <param name="src">Source.</param>
        /// <returns>Destination.</returns>
        public ICode Visit(ICode src)
        {
            var param = src as ParameterCode;
            if (param == null) return src;
            if (param.MetaId == null) return src;
            return new VariableNameCode((string)param.Name);
        }
    }
}

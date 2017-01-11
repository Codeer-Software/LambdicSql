using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class CustomizeParameterToObject : IPartsCustomizer
    {
        bool _isAllowString;

        internal CustomizeParameterToObject() { }

        internal CustomizeParameterToObject(bool isAllowString)
        {
            _isAllowString = isAllowString;
        }

        public CodeParts Custom(CodeParts src)
        {
            var param = src as ParameterParts;
            return param == null ? src : param.ToDisplayValue(_isAllowString);
        }
    }
}

using LambdicSql.BuilderServices.CodeParts;

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

        public Parts Custom(Parts src)
        {
            var param = src as ParameterParts;
            return param == null ? src : param.ToDisplayValue(_isAllowString);
        }
    }
}

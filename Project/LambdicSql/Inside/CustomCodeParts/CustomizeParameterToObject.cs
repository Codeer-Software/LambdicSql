using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class CustomizeParameterToObject : ICodeCustomizer
    {
        bool _isAllowString;

        internal CustomizeParameterToObject() { }

        internal CustomizeParameterToObject(bool isAllowString)
        {
            _isAllowString = isAllowString;
        }

        public Code Custom(Code src)
        {
            var param = src as ParameterCode;
            return param == null ? src : param.ToDisplayValue(_isAllowString);
        }
    }
}

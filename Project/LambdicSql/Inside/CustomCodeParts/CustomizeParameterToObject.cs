using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class CustomizeParameterToObject : ICodeCustomizer
    {
        public Code Custom(Code src)
        {
            var param = src as ParameterCode;
            return param == null ? src : param.ToDisplayValue();
        }
    }
}

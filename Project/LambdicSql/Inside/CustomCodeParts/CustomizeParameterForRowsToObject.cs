using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class CustomizeParameterForRowsToObject : IPartsCustomizer
    {
        public CodeParts Custom(CodeParts src)
        {
            var param = src as ParameterParts;
            return param == null ? src : new RowsParameterParts(param);
        }
    }
}

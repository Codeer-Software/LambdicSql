using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class CustomizeColumnOnly : IPartsCustomizer
    {
        public CodeParts Custom(CodeParts src)
        {
            var col = src as DbColumnParts;
            return col == null ? src : col.ToColumnOnly();
        }
    }
}

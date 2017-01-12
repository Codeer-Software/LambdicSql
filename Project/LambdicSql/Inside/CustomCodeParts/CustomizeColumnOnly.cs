using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class CustomizeColumnOnly : IPartsCustomizer
    {
        public Parts Custom(Parts src)
        {
            var col = src as DbColumnParts;
            return col == null ? src : col.ToColumnOnly();
        }
    }
}

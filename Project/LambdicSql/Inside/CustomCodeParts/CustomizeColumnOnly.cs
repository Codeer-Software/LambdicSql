using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class CustomizeColumnOnly : ICodeCustomizer
    {
        public Code Custom(Code src)
        {
            var col = src as DbColumnCode;
            return col == null ? src : col.ToColumnOnly();
        }
    }
}

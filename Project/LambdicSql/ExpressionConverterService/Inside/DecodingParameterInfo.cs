using LambdicSql.Inside;

namespace LambdicSql.ExpressionConverterService.Inside
{
    class DecodingParameterInfo
    {
        internal MetaId MetadataToken { get; set; }
        internal DbParam Detail { get; set; }
    }
}

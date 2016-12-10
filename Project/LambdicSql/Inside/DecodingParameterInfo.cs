namespace LambdicSql.Inside
{
    class DecodingParameterInfo
    {
        //TODO あー、Modlueの情報がないと正確には一意に表すことができない ていうか、これって他のキャッシュに応用できるよね？
        internal int? MetadataToken { get; set; }
        internal DbParam Detail { get; set; }
    }
}

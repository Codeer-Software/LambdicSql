namespace LambdicSql.BuilderServices.Code.Inside
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

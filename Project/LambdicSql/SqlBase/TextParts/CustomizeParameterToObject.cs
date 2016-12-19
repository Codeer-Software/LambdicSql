namespace LambdicSql.SqlBase.TextParts
{
    class CustomizeParameterToObject : ISqlTextCustomizer
    {
        public SqlText Custom(SqlText src)
        {
            var col = src as ParameterText;
            if (col == null) return src;
            return col.ToDisplayValue();
        }
    }
}

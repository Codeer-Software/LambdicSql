namespace LambdicSql.SqlBase.TextParts
{
    //TODO 厳密に考えると、組み合わせ前にやっちゃったらばグル
    class CustomizeParameterToObject : ISqlTextCustomizer
    {
        public ExpressionElement Custom(ExpressionElement src)
        {
            var col = src as ParameterText;
            if (col == null) return src;
            return col.ToDisplayValue();
        }
    }
}

namespace LambdicSql.SqlBuilder.Sentences.Inside
{
    //TODO 厳密に考えると、組み合わせ前にやっちゃったらばグル
    class CustomizeParameterToObject : ISqlTextCustomizer
    {
        public Sentence Custom(Sentence src)
        {
            var col = src as ParameterText;
            if (col == null) return src;
            return col.ToDisplayValue();
        }
    }
}

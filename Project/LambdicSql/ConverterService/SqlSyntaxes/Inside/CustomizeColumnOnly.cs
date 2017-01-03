using LambdicSql.SqlBuilder;
using LambdicSql.SqlBuilder.Sentences;
using LambdicSql.SqlBuilder.Sentences.Inside;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class CustomizeColumnOnly : ISqlTextCustomizer
    {
        public Sentence Custom(Sentence src)
        {
            var col = src as DbColumnText;
            if (col == null) return src;
            return col.ToColumnOnly();
        }
    }
}

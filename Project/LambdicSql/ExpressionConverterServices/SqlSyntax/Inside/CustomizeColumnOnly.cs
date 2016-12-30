﻿using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.ExpressionConverterServices.SqlSyntax.Inside
{
    class CustomizeColumnOnly : ISqlTextCustomizer
    {
        public ExpressionElement Custom(ExpressionElement src)
        {
            var col = src as DbColumnText;
            if (col == null) return src;
            return col.ToColumnOnly();
        }
    }
}

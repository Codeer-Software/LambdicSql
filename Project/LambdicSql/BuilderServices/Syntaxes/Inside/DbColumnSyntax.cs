using LambdicSql.ConverterServices.Inside;

namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class DbColumnSyntax : Syntax
    {
        ColumnInfo _col;
        string _front = string.Empty;
        string _back = string.Empty;
        bool _columnOnly;

        internal DbColumnSyntax(ColumnInfo col)
        {
            _col = col;
        }

        internal DbColumnSyntax(ColumnInfo col, bool columnOnly)
        {
            _col = col;
            _columnOnly = columnOnly;
        }

        DbColumnSyntax(ColumnInfo col, bool columnOnly, string front, string back)
        {
            _col = col;
            _front = front;
            _back = back;
            _columnOnly = columnOnly;
        }

        internal Syntax ToColumnOnly() => new DbColumnSyntax(_col, true, _front, _back);

        string ColumnName => _columnOnly ? _col.SqlColumnName : _col.SqlFullName;

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => SyntaxUtils.GetIndent(indent) + _front + ColumnName + _back;

        public override Syntax ConcatAround(string front, string back) => new DbColumnSyntax(_col, _columnOnly, front + _front, _back + back);

        public override Syntax ConcatToFront(string front) => new DbColumnSyntax(_col, _columnOnly, front + _front, _back);

        public override Syntax ConcatToBack(string back) => new DbColumnSyntax(_col, _columnOnly, _front, _back + back);

        public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}

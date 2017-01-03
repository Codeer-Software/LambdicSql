using LambdicSql.ConverterServices;

namespace LambdicSql.BuilderServices.Parts.Inside
{
    class DbColumnParts : BuildingParts
    {
        ColumnInfo _col;
        string _front = string.Empty;
        string _back = string.Empty;
        bool _columnOnly;

        internal DbColumnParts(ColumnInfo col)
        {
            _col = col;
        }

        internal DbColumnParts(ColumnInfo col, bool columnOnly)
        {
            _col = col;
            _columnOnly = columnOnly;
        }

        DbColumnParts(ColumnInfo col, bool columnOnly, string front, string back)
        {
            _col = col;
            _front = front;
            _back = back;
            _columnOnly = columnOnly;
        }

        internal BuildingParts ToColumnOnly() => new DbColumnParts(_col, true, _front, _back);

        string ColumnName => _columnOnly ? _col.SqlColumnName : _col.SqlFullName;

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => BuildingPartsUtils.GetIndent(indent) + _front + ColumnName + _back;

        public override BuildingParts ConcatAround(string front, string back) => new DbColumnParts(_col, _columnOnly, front + _front, _back + back);

        public override BuildingParts ConcatToFront(string front) => new DbColumnParts(_col, _columnOnly, front + _front, _back);

        public override BuildingParts ConcatToBack(string back) => new DbColumnParts(_col, _columnOnly, _front, _back + back);

        public override BuildingParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}

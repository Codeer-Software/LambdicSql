using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices.Inside;

namespace LambdicSql.Inside.CustomCodeParts
{
    class DbColumnParts : Parts
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

        internal Parts ToColumnOnly() => new DbColumnParts(_col, true, _front, _back);

        string ColumnName => _columnOnly ? _col.SqlColumnName : _col.SqlFullName;

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(indent) + _front + ColumnName + _back;

        public override Parts ConcatAround(string front, string back) => new DbColumnParts(_col, _columnOnly, front + _front, _back + back);

        public override Parts ConcatToFront(string front) => new DbColumnParts(_col, _columnOnly, front + _front, _back);

        public override Parts ConcatToBack(string back) => new DbColumnParts(_col, _columnOnly, _front, _back + back);

        public override Parts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}

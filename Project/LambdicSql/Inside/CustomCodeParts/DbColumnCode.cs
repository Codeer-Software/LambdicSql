using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices.Inside;

namespace LambdicSql.Inside.CustomCodeParts
{
    class DbColumnCode : Code
    {
        ColumnInfo _col;
        string _front = string.Empty;
        string _back = string.Empty;
        bool _columnOnly;

        internal DbColumnCode(ColumnInfo col)
        {
            _col = col;
        }

        internal DbColumnCode(ColumnInfo col, bool columnOnly)
        {
            _col = col;
            _columnOnly = columnOnly;
        }

        DbColumnCode(ColumnInfo col, bool columnOnly, string front, string back)
        {
            _col = col;
            _front = front;
            _back = back;
            _columnOnly = columnOnly;
        }

        internal Code ToColumnOnly() => new DbColumnCode(_col, true, _front, _back);

        string ColumnName => _columnOnly ? _col.SqlColumnName : _col.SqlFullName;

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(indent) + _front + ColumnName + _back;

        public override Code ConcatAround(string front, string back) => new DbColumnCode(_col, _columnOnly, front + _front, _back + back);

        public override Code ConcatToFront(string front) => new DbColumnCode(_col, _columnOnly, front + _front, _back);

        public override Code ConcatToBack(string back) => new DbColumnCode(_col, _columnOnly, _front, _back + back);

        public override Code Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}

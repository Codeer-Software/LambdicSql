using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class DbColumnCode : ICode
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

        internal ICode ToColumnOnly() => new DbColumnCode(_col, true, _front, _back);

        string ColumnName => _columnOnly ? _col.SqlColumnName : _col.SqlFullName;

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => true;

        public string ToString(BuildingContext context) => PartsUtils.GetIndent(context.Indent) + _front + ColumnName + _back;

        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);
    }
}

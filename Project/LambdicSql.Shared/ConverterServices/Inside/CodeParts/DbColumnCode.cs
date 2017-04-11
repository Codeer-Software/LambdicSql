using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class DbColumnCode : ICode
    {
        ColumnInfo _col;
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
        
        internal ICode ToColumnOnly() => new DbColumnCode(_col, true);

        internal string ColumnName => _columnOnly ? _col.SqlColumnName : _col.SqlFullName;

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => true;

        public string ToString(BuildingContext context) => PartsUtils.GetIndent(context.Indent) + ColumnName;

        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);
    }
}

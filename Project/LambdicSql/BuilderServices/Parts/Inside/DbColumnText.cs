using LambdicSql.ConverterServices;
using System.Linq;

namespace LambdicSql.BuilderServices.Parts.Inside
{
    class DbColumnText : BuildingParts
    {
        string _front = string.Empty;
        string _back = string.Empty;
        bool _columnOnly;

        internal ColumnInfo Info { get; private set; }

        string ColumnName => _columnOnly ? Info.SqlColumnName : Info.SqlFullName;

        internal BuildingParts ToColumnOnly() =>
            new DbColumnText(Info, true, _front, _back);

        internal DbColumnText(ColumnInfo info)
        {
            Info = info;
        }

        internal DbColumnText(ColumnInfo info, bool columnOnly)
        {
            Info = info;
            _columnOnly = columnOnly;
        }

        DbColumnText(ColumnInfo info, bool columnOnly, string front, string back)
        {
            Info = info;
            _front = front;
            _back = back;
            _columnOnly = columnOnly;
        }

        public override bool IsSingleLine(BuildingContext context) => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + ColumnName + _back;

        public override BuildingParts ConcatAround(string front, string back) 
            => new DbColumnText(Info, _columnOnly, front + _front, _back + back);

        public override BuildingParts ConcatToFront(string front) 
            => new DbColumnText(Info, _columnOnly, front + _front, _back);

        public override BuildingParts ConcatToBack(string back) 
            => new DbColumnText(Info, _columnOnly, _front, _back + back);

        public override BuildingParts Customize(ISqlTextCustomizer customizer) 
            => customizer.Custom(this);
    }
}

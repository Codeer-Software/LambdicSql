using LambdicSql.ConverterService;
using System.Linq;

namespace LambdicSql.SqlBuilder.Parts.Inside
{
    class DbTableText : BuildingParts
    {
        internal TableInfo Info { get; private set; }
        string _front = string.Empty;
        string _back = string.Empty;

        internal DbTableText(TableInfo info)
        {
            Info = info;
        }

        DbTableText(TableInfo info, string front, string back)
        {
            Info = info;
            _front = front;
            _back = back;
        }

        public override bool IsSingleLine(SqlBuildingContext context) => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
            => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + Info.SqlFullName + _back;

        public override BuildingParts ConcatAround(string front, string back) 
            => new DbTableText(Info, front + _front, _back + back);

        public override BuildingParts ConcatToFront(string front) 
            => new DbTableText(Info, front + _front, _back);

        public override BuildingParts ConcatToBack(string back) 
            => new DbTableText(Info, _front, _back + back);

        public override BuildingParts Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}

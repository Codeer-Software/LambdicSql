using LambdicSql.ConverterServices.Inside;

namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class DbTableSyntax : TextPartsBase
    {
        string _front = string.Empty;
        string _back = string.Empty;

        internal DbTableSyntax(TableInfo info)
        {
            Info = info;
        }

        DbTableSyntax(TableInfo info, string front, string back)
        {
            Info = info;
            _front = front;
            _back = back;
        }

        internal TableInfo Info { get; private set; }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => SyntaxUtils.GetIndent(indent) + _front + Info.SqlFullName + _back;

        public override TextPartsBase ConcatAround(string front, string back) => new DbTableSyntax(Info, front + _front, _back + back);

        public override TextPartsBase ConcatToFront(string front) => new DbTableSyntax(Info, front + _front, _back);

        public override TextPartsBase ConcatToBack(string back) => new DbTableSyntax(Info, _front, _back + back);

        public override TextPartsBase Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}

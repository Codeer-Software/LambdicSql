using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices.Inside;

namespace LambdicSql.Inside.CustomCodeParts
{
    class DbTableParts : Parts
    {
        string _front = string.Empty;
        string _back = string.Empty;

        internal DbTableParts(TableInfo info)
        {
            Info = info;
        }

        DbTableParts(TableInfo info, string front, string back)
        {
            Info = info;
            _front = front;
            _back = back;
        }

        internal TableInfo Info { get; private set; }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(indent) + _front + Info.SqlFullName + _back;

        public override Parts ConcatAround(string front, string back) => new DbTableParts(Info, front + _front, _back + back);

        public override Parts ConcatToFront(string front) => new DbTableParts(Info, front + _front, _back);

        public override Parts ConcatToBack(string back) => new DbTableParts(Info, _front, _back + back);

        public override Parts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}

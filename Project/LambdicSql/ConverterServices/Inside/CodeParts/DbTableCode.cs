using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class DbTableCode : Code
    {
        string _front = string.Empty;
        string _back = string.Empty;

        internal DbTableCode(TableInfo info)
        {
            Info = info;
        }

        DbTableCode(TableInfo info, string front, string back)
        {
            Info = info;
            _front = front;
            _back = back;
        }

        internal TableInfo Info { get; private set; }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(indent) + _front + Info.SqlFullName + _back;

        public override Code ConcatAround(string front, string back) => new DbTableCode(Info, front + _front, _back + back);

        public override Code ConcatToFront(string front) => new DbTableCode(Info, front + _front, _back);

        public override Code ConcatToBack(string back) => new DbTableCode(Info, _front, _back + back);

        public override Code Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}

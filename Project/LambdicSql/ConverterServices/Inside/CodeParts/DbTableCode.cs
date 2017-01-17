using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class DbTableCode : ICode
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

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => true;

        public string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(indent) + _front + Info.SqlFullName + _back;

        public ICode Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}

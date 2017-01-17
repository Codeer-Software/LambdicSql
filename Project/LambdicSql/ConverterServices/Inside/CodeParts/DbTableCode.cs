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

        internal TableInfo Info { get; }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => true;

        public string ToString(BuildingContext context) => PartsUtils.GetIndent(context.Indent) + _front + Info.SqlFullName + _back;

        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);
    }
}

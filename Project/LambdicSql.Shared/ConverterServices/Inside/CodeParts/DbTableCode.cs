using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class DbTableCode : ICode
    {
        internal DbTableCode(TableInfo info)
        {
            Info = info;
        }

        internal TableInfo Info { get; }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => true;

        public string ToString(BuildingContext context) => PartsUtils.GetIndent(context.Indent) + Info.SqlFullName;

        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);
    }
}

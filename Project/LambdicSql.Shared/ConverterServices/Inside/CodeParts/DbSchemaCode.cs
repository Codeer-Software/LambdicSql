using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class DbSchemaCode : ICode
    {
        internal string Text { get; }

        internal DbSchemaCode(string schema)
        {
            Text = schema;
        }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => true;

        public string ToString(BuildingContext context) => PartsUtils.GetIndent(context.Indent) + Text;

        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);
    }
}

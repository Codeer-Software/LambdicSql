using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    internal class QueryCode : Code
    {
        Code _core;

        internal QueryCode(Code core)
        {
            _core = core;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(false, indent, context);

        public override Code ConcatAround(string front, string back) => new QueryCode(_core.ConcatAround(front, back));

        public override Code ConcatToFront(string front) => new QueryCode(_core.ConcatToFront(front));

        public override Code ConcatToBack(string back) => new QueryCode(_core.ConcatToBack(back));

        public override Code Customize(ICodeCustomizer customizer) => new QueryCode(_core.Customize(customizer));
    }
}

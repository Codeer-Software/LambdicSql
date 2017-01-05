using LambdicSql.ConverterServices.Inside;

namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class SelectClauseSyntax : TextPartsBase
    {
        TextPartsBase _core;
        ObjectCreateInfo _createInfo;

        internal SelectClauseSyntax(ObjectCreateInfo createInfo, TextPartsBase core)
        {
            _core = core;
            _createInfo = createInfo;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(isTopLevel, indent, context);

        public override TextPartsBase ConcatAround(string front, string back) => new SelectClauseSyntax(_createInfo, _core.ConcatAround(front, back));

        public override TextPartsBase ConcatToFront(string front) => new SelectClauseSyntax(_createInfo, _core.ConcatToFront(front));

        public override TextPartsBase ConcatToBack(string back) => new SelectClauseSyntax(_createInfo, _core.ConcatToBack(back));

        public override TextPartsBase Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}

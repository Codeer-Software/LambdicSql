using LambdicSql.ConverterServices.Inside;

namespace LambdicSql.BuilderServices.Parts.Inside
{
    class SelectClauseParts : BuildingParts
    {
        BuildingParts _core;
        ObjectCreateInfo _createInfo;

        internal SelectClauseParts(ObjectCreateInfo createInfo, BuildingParts core)
        {
            _core = core;
            _createInfo = createInfo;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(isTopLevel, indent, context);

        public override BuildingParts ConcatAround(string front, string back) => new SelectClauseParts(_createInfo, _core.ConcatAround(front, back));

        public override BuildingParts ConcatToFront(string front) => new SelectClauseParts(_createInfo, _core.ConcatToFront(front));

        public override BuildingParts ConcatToBack(string back) => new SelectClauseParts(_createInfo, _core.ConcatToBack(back));

        public override BuildingParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}

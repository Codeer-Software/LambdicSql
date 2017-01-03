using LambdicSql.ConverterService;

namespace LambdicSql.SqlBuilder.Parts.Inside
{
    class SelectClauseText : BuildingParts
    {
        BuildingParts _core;
        ObjectCreateInfo _createInfo;

        internal SelectClauseText(ObjectCreateInfo createInfo, BuildingParts core)
        {
            _core = core;
            _createInfo = createInfo;
        }

        public override bool IsSingleLine(SqlBuildingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => _core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
        {
            return _core.ToString(isTopLevel, indent, context);
        }

        public override BuildingParts ConcatAround(string front, string back) => new SelectClauseText(_createInfo, _core.ConcatAround(front, back));

        public override BuildingParts ConcatToFront(string front) => new SelectClauseText(_createInfo, _core.ConcatToFront(front));

        public override BuildingParts ConcatToBack(string back) => new SelectClauseText(_createInfo, _core.ConcatToBack(back));

        public override BuildingParts Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}

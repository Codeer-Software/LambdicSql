namespace LambdicSql.BuilderServices.Parts.Inside
{
    abstract class TextWrapper : BuildingParts
    {
        protected BuildingParts Core { get; private set; }

        public TextWrapper(BuildingParts core)
        {
            Core = core;
        }

        public override bool IsSingleLine(BuildingContext context) => Core.IsSingleLine(context);

        public override bool IsEmpty => Core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) 
            => Core.ToString(isTopLevel, indent, context);
    }
}

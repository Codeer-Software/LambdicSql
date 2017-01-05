namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class RecursiveTargetSyntax : Syntax
    {
        Syntax _core;

        internal RecursiveTargetSyntax(Syntax core)
        {
            _core = core;
        }

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => context.Option.ExistRecursiveClause ?
                _core.ConcatToFront("RECURSIVE ").ToString(isTopLevel, indent, context):
                _core.ToString(isTopLevel, indent, context);

        public override Syntax ConcatAround(string front, string back) => new RecursiveTargetSyntax(_core.ConcatAround(front, back));

        public override Syntax ConcatToFront(string front) => new RecursiveTargetSyntax(_core.ConcatToFront(front));

        public override Syntax ConcatToBack(string back) => new RecursiveTargetSyntax(_core.ConcatToBack(back));

        public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}

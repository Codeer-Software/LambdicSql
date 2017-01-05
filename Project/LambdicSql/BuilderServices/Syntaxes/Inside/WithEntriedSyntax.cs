namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class WithEntriedSyntax : Syntax
    {
        Syntax _core;
        string[] _names;

        internal WithEntriedSyntax(Syntax core, string[] names)
        {
            _core = core;
            _names = names;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
        {
            foreach (var e in _names) context.WithEntied[e] = true;
            return _core.ToString(isTopLevel, indent, context);
        }

        public override Syntax ConcatAround(string front, string back) => new WithEntriedSyntax(_core.ConcatAround(front, back), _names);

        public override Syntax ConcatToFront(string front) => new WithEntriedSyntax(_core.ConcatToFront(front), _names);

        public override Syntax ConcatToBack(string back) => new WithEntriedSyntax(_core.ConcatToBack(back), _names);

        public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}

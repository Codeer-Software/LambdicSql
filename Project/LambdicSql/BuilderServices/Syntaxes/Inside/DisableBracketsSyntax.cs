namespace LambdicSql.BuilderServices.Syntaxes.Inside
{ 
    //TODO そもそも括弧つけの手法が見直せたら良い
    class DisableBracketsSyntax : Syntax
    {
        Syntax _core;

        internal DisableBracketsSyntax(Syntax core)
        {
            _core = core;
        }

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => _core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(false, indent, context);

        public override Syntax ConcatAround(string front, string back) => this;

        public override Syntax ConcatToFront(string front) => new DisableBracketsSyntax(_core.ConcatToFront(front));

        public override Syntax ConcatToBack(string back) => new DisableBracketsSyntax(_core.ConcatToBack(back));

        public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}

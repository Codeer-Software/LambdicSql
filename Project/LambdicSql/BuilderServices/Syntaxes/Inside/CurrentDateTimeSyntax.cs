namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class CurrentDateTimeSyntax : Syntax
    {
        string _front = string.Empty;
        string _back = string.Empty;
        string _core;

        internal CurrentDateTimeSyntax(string core)
        {
            _core = core;
        }

        CurrentDateTimeSyntax(string core, string front, string back)
        {
            _core = core;
            _front = front;
            _back = back;
        }

        public override bool IsSingleLine(BuildingContext context) => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => SyntaxUtils.GetIndent(indent) + _front + "CURRENT" + context.Option.CurrentDateTimeSeparator + _core + _back;

        public override Syntax ConcatAround(string front, string back) => new CurrentDateTimeSyntax(_core, front + _front, _back + back);

        public override Syntax ConcatToFront(string front) => new CurrentDateTimeSyntax(_core, front + _front, _back);

        public override Syntax ConcatToBack(string back) => new CurrentDateTimeSyntax(_core, _front, _back + back);

        public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}

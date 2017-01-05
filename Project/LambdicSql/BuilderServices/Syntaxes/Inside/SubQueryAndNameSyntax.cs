namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class SubQueryAndNameSyntax : Syntax
    {
        string _front = string.Empty;
        string _back = string.Empty;
        string _body;
        Syntax _define;

        internal SubQueryAndNameSyntax(string body, Syntax table)
        {
            _body = body;
            _define = new HSyntax(table, _body) { Separator = " ", EnableChangeLine = false };
        }

        SubQueryAndNameSyntax(string body, Syntax define, string front, string back)
        {
            _body = body;
            _define = define;
            _front = front;
            _back = back;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => context.WithEntied.ContainsKey(_body) ? true : _define.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => context.WithEntied.ContainsKey(_body) ?
                    (SyntaxUtils.GetIndent(indent) + _front + _body + _back) :
                    _define.ToString(isTopLevel, indent, context);

        public override Syntax ConcatAround(string front, string back) => new SubQueryAndNameSyntax(_body, _define.ConcatAround(front, back), front + _front, _back + back);

        public override Syntax ConcatToFront(string front) => new SubQueryAndNameSyntax(_body, _define.ConcatToFront(front), front + _front, _back);

        public override Syntax ConcatToBack(string back) => new SubQueryAndNameSyntax(_body, _define.ConcatToBack(back), _front, _back + back);

        public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}

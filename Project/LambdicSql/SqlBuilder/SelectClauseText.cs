namespace LambdicSql.SqlBase.TextParts
{
    class SelectClauseText : ExpressionElement
    {
        ExpressionElement _core;
        ObjectCreateInfo _createInfo;

        internal SelectClauseText(ObjectCreateInfo createInfo, ExpressionElement core)
        {
            _core = core;
            _createInfo = createInfo;
        }

        public override bool IsSingleLine(ExpressionConvertingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => _core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
        {
            //remember creat info.
            if (context.ObjectCreateInfo == null) context.ObjectCreateInfo = _createInfo;
            return _core.ToString(isTopLevel, indent, context);
        }

        public override ExpressionElement ConcatAround(string front, string back) => new SelectClauseText(_createInfo, _core.ConcatAround(front, back));

        public override ExpressionElement ConcatToFront(string front) => new SelectClauseText(_createInfo, _core.ConcatToFront(front));

        public override ExpressionElement ConcatToBack(string back) => new SelectClauseText(_createInfo, _core.ConcatToBack(back));

        public override ExpressionElement Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}

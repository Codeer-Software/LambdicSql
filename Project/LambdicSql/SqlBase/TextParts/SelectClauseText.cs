namespace LambdicSql.SqlBase.TextParts
{
    internal class SelectClauseText : SqlText
    {
        SqlText _core;
        ObjectCreateInfo _createInfo;

        internal SelectClauseText(ObjectCreateInfo createInfo, SqlText core)
        {
            _core = core;
            _createInfo = createInfo;
        }

        public override bool IsSingleLine => _core.IsSingleLine;

        public override bool IsEmpty => _core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, SqlConvertingContext context)
        {
            //remember creat info.
            if (context.ObjectCreateInfo == null) context.ObjectCreateInfo = _createInfo;
            return _core.ToString(isTopLevel, indent, context);
        }

        public override SqlText ConcatAround(string front, string back) => new SelectClauseText(_createInfo, _core.ConcatAround(front, back));

        public override SqlText ConcatToFront(string front) => new SelectClauseText(_createInfo, _core.ConcatToFront(front));

        public override SqlText ConcatToBack(string back) => new SelectClauseText(_createInfo, _core.ConcatToBack(back));

        public override SqlText Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}

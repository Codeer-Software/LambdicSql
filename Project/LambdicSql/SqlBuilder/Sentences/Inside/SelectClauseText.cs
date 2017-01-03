using LambdicSql.ConverterService;

namespace LambdicSql.SqlBuilder.Sentences.Inside
{
    class SelectClauseText : Sentence
    {
        Sentence _core;
        ObjectCreateInfo _createInfo;

        internal SelectClauseText(ObjectCreateInfo createInfo, Sentence core)
        {
            _core = core;
            _createInfo = createInfo;
        }

        public override bool IsSingleLine(SqlBuildingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => _core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
        {
            //remember creat info.
            if (context.ObjectCreateInfo == null) context.ObjectCreateInfo = _createInfo;
            return _core.ToString(isTopLevel, indent, context);
        }

        public override Sentence ConcatAround(string front, string back) => new SelectClauseText(_createInfo, _core.ConcatAround(front, back));

        public override Sentence ConcatToFront(string front) => new SelectClauseText(_createInfo, _core.ConcatToFront(front));

        public override Sentence ConcatToBack(string back) => new SelectClauseText(_createInfo, _core.ConcatToBack(back));

        public override Sentence Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}

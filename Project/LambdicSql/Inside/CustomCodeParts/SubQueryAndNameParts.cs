using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class SubQueryAndNameParts : CodeParts
    {
        string _front = string.Empty;
        string _back = string.Empty;
        string _body;
        CodeParts _define;

        internal SubQueryAndNameParts(string body, CodeParts table)
        {
            _body = body;
            _define = new HParts(table, _body) { Separator = " ", EnableChangeLine = false };
        }

        SubQueryAndNameParts(string body, CodeParts define, string front, string back)
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
                    (PartsUtils.GetIndent(indent) + _front + _body + _back) :
                    _define.ToString(isTopLevel, indent, context);

        public override CodeParts ConcatAround(string front, string back) => new SubQueryAndNameParts(_body, _define.ConcatAround(front, back), front + _front, _back + back);

        public override CodeParts ConcatToFront(string front) => new SubQueryAndNameParts(_body, _define.ConcatToFront(front), front + _front, _back);

        public override CodeParts ConcatToBack(string back) => new SubQueryAndNameParts(_body, _define.ConcatToBack(back), _front, _back + back);

        public override CodeParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}

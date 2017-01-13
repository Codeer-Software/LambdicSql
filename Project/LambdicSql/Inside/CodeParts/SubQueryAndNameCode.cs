using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CodeParts
{
    class SubQueryAndNameCode : Code
    {
        string _front = string.Empty;
        string _back = string.Empty;
        string _body;
        Code _define;

        internal SubQueryAndNameCode(string body, Code table)
        {
            _body = body;
            _define = new HCode(table, _body) { Separator = " ", EnableChangeLine = false };
        }

        SubQueryAndNameCode(string body, Code define, string front, string back)
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

        public override Code ConcatAround(string front, string back) => new SubQueryAndNameCode(_body, _define.ConcatAround(front, back), front + _front, _back + back);

        public override Code ConcatToFront(string front) => new SubQueryAndNameCode(_body, _define.ConcatToFront(front), front + _front, _back);

        public override Code ConcatToBack(string back) => new SubQueryAndNameCode(_body, _define.ConcatToBack(back), _front, _back + back);

        public override Code Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}

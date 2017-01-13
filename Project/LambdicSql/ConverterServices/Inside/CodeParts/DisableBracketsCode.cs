using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{ 
    class DisableBracketsCode : Code
    {
        Code _core;

        internal DisableBracketsCode(Code core)
        {
            _core = core;
        }

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => _core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(false, indent, context);

        public override Code ConcatAround(string front, string back) => this;

        public override Code ConcatToFront(string front) => new DisableBracketsCode(_core.ConcatToFront(front));

        public override Code ConcatToBack(string back) => new DisableBracketsCode(_core.ConcatToBack(back));

        public override Code Customize(ICodeCustomizer customizer) => new DisableBracketsCode(_core.Customize(customizer));
    }
}

using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.BasicCode;

namespace LambdicSql.ConverterServices.Inside.Code
{ 
    class DisableBracketsCode : BuilderServices.BasicCode.Code
    {
        BuilderServices.BasicCode.Code _core;

        internal DisableBracketsCode(BuilderServices.BasicCode.Code core)
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

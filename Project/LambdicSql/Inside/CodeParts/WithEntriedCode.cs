using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CodeParts
{
    class WithEntriedCode : Code
    {
        Code _core;
        string[] _names;

        internal WithEntriedCode(Code core, string[] names)
        {
            _core = core;
            _names = names;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
        {
            foreach (var e in _names) context.WithEntied[e] = true;
            return _core.ToString(isTopLevel, indent, context);
        }

        public override Code Customize(ICodeCustomizer customizer) => new WithEntriedCode(_core.Customize(customizer), _names);
    }
}

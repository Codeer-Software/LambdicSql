using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CodeParts
{
    class WithEntriedCode : ICode
    {
        ICode _core;
        string[] _names;

        internal WithEntriedCode(ICode core, string[] names)
        {
            _core = core;
            _names = names;
        }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public string ToString(BuildingContext context)
        {
            foreach (var e in _names) context.WithEntied[e] = true;
            return _core.ToString(context);
        }

        public ICode Customize(ICodeCustomizer customizer) => new WithEntriedCode(_core.Customize(customizer), _names);
    }
}

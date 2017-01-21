using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;
using System.Collections.Generic;

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
            Dictionary<string, bool> withEntied = null;
            object obj;
            if (context.UserData.TryGetValue(typeof(WithEntriedCode), out obj))
            {
                withEntied = (Dictionary<string, bool>)obj;
            }
            else
            { 
                withEntied = new Dictionary<string, bool>();
                context.UserData[typeof(WithEntriedCode)] = withEntied;
            }

            foreach (var e in _names) withEntied[e] = true;
            return _core.ToString(context);
        }

        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;
            return new WithEntriedCode(_core.Accept(customizer), _names);
        }
    }
}

using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Code;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Code.Inside.PartsFactoryUtils;
using System;
using System.Collections.Generic;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class SpecialNameClauseConverterAttribute : SymbolConverterMethodAttribute
    {
        public string Name { get; set; }

        static Dictionary<MetaId, string> _specialNames = new Dictionary<MetaId, string>();

        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var index = expression.SkipMethodChain(0);
            var member = expression.Arguments[index] as MemberExpression;
            string name;
            var id = new MetaId(member.Member);
            if (!_specialNames.TryGetValue(id, out name))
            {
                var specialName = member.Member.GetCustomAttributes(typeof(ConvertingNameAttribute), true);
                name = (specialName.Length == 1) ? ((ConvertingNameAttribute)specialName[0]).Name : string.Empty;
                _specialNames[id] = name;
            }

            name = string.IsNullOrEmpty(name) ? member.Member.Name : name;
            return Clause(Name, name);
        }
    }
}

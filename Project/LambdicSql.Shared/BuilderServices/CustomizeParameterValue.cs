using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices.Inside.CodeParts;
using System.Collections.Generic;

namespace LambdicSql.BuilderServices
{
    class CustomizeParameterValue : ICodeCustomizer
    {
        Dictionary<string, object> _values;

        internal CustomizeParameterValue(Dictionary<string, object> values)
        {
            _values = values;
        }

        public ICode Visit(ICode src)
        {
            var param = src as ParameterCode;
            if (param == null) return src;

            object val;
            if (!_values.TryGetValue(param.Name, out val)) return src;

            return new ParameterCode(param.Name, param.MetaId, param.Param.ChangeValue(val));
        }
    }
}

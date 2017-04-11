using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;
using System;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class ParameterCode : ICode
    {
        internal string Name { get; private set; }
        internal MetaId MetaId { get; private set; }
        internal object Value => _param.Value;
        internal IDbParam Param => _param;

        IDbParam _param;
        bool _displayValue;

        internal ParameterCode(object value)
        {
            Name = null;
            MetaId = null;
            _param = new DbParamValueOnly() {Value = value };
        }

        internal ParameterCode(string name, MetaId metaId, IDbParam param)
        {
            Name = name;
            MetaId = metaId;
            _param = param;
        }

        ParameterCode(string name, MetaId metaId, IDbParam param, bool displayValue)
        {
            Name = name;
            MetaId = metaId;
            _param = param;
            _displayValue = displayValue;
        }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => true;

        public string ToString(BuildingContext context) => PartsUtils.GetIndent(context.Indent) + GetDisplayText(context);

        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);

        internal ICode ToDisplayValue() => new ParameterCode(Name, MetaId, _param, true);

        string GetDisplayText(BuildingContext context)
        {
            if (_displayValue)
            {
                if (Value == null)
                {
                    return "NULL";
                }

                var type = Value.GetType();
                if (type == typeof(string) || 
                    type == typeof(DateTime) ||
                    type == typeof(DateTimeOffset) ||
                    type == typeof(TimeSpan))
                {
                    return "'" + Value + "'";
                }
                if (type == typeof(DateTime?))
                {
                    return "'" + ((DateTime?)Value).Value + "'";
                }
                if (type == typeof(DateTimeOffset?))
                {
                    return "'" + ((DateTimeOffset?)Value).Value + "'";
                }
                if (type == typeof(TimeSpan?))
                {
                    return "'" + ((TimeSpan?)Value).Value + "'";
                }
                return Value.ToString();
            }
            return context.ParameterInfo.Push(_param.Value, Name, MetaId, _param);
        }
    }
}

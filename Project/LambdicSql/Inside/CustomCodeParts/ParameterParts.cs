using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.Parts;
using LambdicSql.ConverterServices.Inside;
using System;

namespace LambdicSql.Inside.CustomCodeParts
{
    class ParameterParts : CodeParts
    {
        internal string Name { get; private set; }
        internal MetaId MetaId { get; private set; }
        internal object Value => _param.Value;

        DbParam _param;
        string _front = string.Empty;
        string _back = string.Empty;
        bool _displayValue;
        bool _isAllowString;

        internal ParameterParts(object value)
        {
            Name = null;
            MetaId = null;
            _param = new DbParam() {Value = value };
        }

        internal ParameterParts(string name = null, MetaId metaId = null, DbParam param = null)
        {
            Name = name;
            MetaId = metaId;
            _param = param;
        }

        ParameterParts(string name, MetaId metaId, DbParam param, string front, string back, bool displayValue, bool isAllowString)
        {
            Name = name;
            MetaId = metaId;
            _param = param;
            _front = front;
            _back = back;
            _displayValue = displayValue;
            _isAllowString = isAllowString;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(indent) + _front + GetDisplayText(context) + _back;

        public override CodeParts ConcatAround(string front, string back) => new ParameterParts(Name, MetaId, _param, front + _front, _back + back, _displayValue, _isAllowString);

        public override CodeParts ConcatToFront(string front) => new ParameterParts(Name, MetaId, _param, front + _front, _back, _displayValue, _isAllowString);

        public override CodeParts ConcatToBack(string back) => new ParameterParts(Name, MetaId, _param, _front, _back + back, _displayValue, _isAllowString);

        public override CodeParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);

        internal CodeParts ToDisplayValue(bool isAllowString) => new ParameterParts(Name, MetaId, _param, _front, _back, true, isAllowString);

        string GetDisplayText(BuildingContext context)
        {
            if (_displayValue)
            {
                if (Value == null)
                {
                    return "NULL";
                }

                var type = Value.GetType();
                if (type == typeof(DateTime) ||
                    type == typeof(DateTimeOffset) ||
                    type == typeof(TimeSpan))
                {
                    return "'" + Value + "'";
                }
                if (type == typeof(string))
                {
                    if (!_isAllowString) throw new NotSupportedException();
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

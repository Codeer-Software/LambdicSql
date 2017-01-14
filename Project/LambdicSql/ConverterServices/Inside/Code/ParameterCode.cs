﻿using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.BasicCode;
using System;

namespace LambdicSql.ConverterServices.Inside.Code
{
    class ParameterCode : BuilderServices.BasicCode.Code
    {
        internal string Name { get; private set; }
        internal MetaId MetaId { get; private set; }
        internal object Value => _param.Value;

        DbParam _param;
        string _front = string.Empty;
        string _back = string.Empty;
        bool _displayValue;

        internal ParameterCode(object value)
        {
            Name = null;
            MetaId = null;
            _param = new DbParam() {Value = value };
        }

        internal ParameterCode(string name = null, MetaId metaId = null, DbParam param = null)
        {
            Name = name;
            MetaId = metaId;
            _param = param;
        }

        ParameterCode(string name, MetaId metaId, DbParam param, string front, string back, bool displayValue)
        {
            Name = name;
            MetaId = metaId;
            _param = param;
            _front = front;
            _back = back;
            _displayValue = displayValue;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(indent) + _front + GetDisplayText(context) + _back;

        public override Code ConcatAround(string front, string back) => new ParameterCode(Name, MetaId, _param, front + _front, _back + back, _displayValue);

        public override Code ConcatToFront(string front) => new ParameterCode(Name, MetaId, _param, front + _front, _back, _displayValue);

        public override Code ConcatToBack(string back) => new ParameterCode(Name, MetaId, _param, _front, _back + back, _displayValue);

        public override Code Customize(ICodeCustomizer customizer) => customizer.Custom(this);

        internal Code ToDisplayValue() => new ParameterCode(Name, MetaId, _param, _front, _back, true);

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
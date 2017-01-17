using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;
using System.Collections.Generic;

namespace LambdicSql.Inside.CodeParts
{
    class SubQueryAndNameCode : ICode
    {
        string _front = string.Empty;
        string _back = string.Empty;
        string _body;
        ICode _define;

        internal SubQueryAndNameCode(string body, ICode table)
        {
            _body = body;
            _define = new HCode(table, _body.ToCode()) { Separator = " ", EnableChangeLine = false };
        }

        SubQueryAndNameCode(string body, ICode define, string front, string back)
        {
            _body = body;
            _define = define;
            _front = front;
            _back = back;
        }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context)
        {
            object obj;
            if (!context.UserData.TryGetValue(typeof(WithEntriedCode), out obj))
            {
                return _define.IsSingleLine(context);
            }
            var withEntied = (Dictionary<string, bool>)obj;
            return withEntied.ContainsKey(_body) ? true : _define.IsSingleLine(context);
        }

        public string ToString(BuildingContext context)
        {
            object obj;
            if (!context.UserData.TryGetValue(typeof(WithEntriedCode), out obj))
            {
                return _define.ToString(context);
            }
            var withEntied = (Dictionary<string, bool>)obj;
            return withEntied.ContainsKey(_body) ?
                    (PartsUtils.GetIndent(context.Indent) + _front + _body + _back) :
                    _define.ToString(context);
        }

        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);
    }
}

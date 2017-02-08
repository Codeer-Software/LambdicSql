using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.MultiplatformCompatibe;
using LambdicSql.BuilderServices.Inside;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.Inside
{
    class GeneralStyleConverterCore
    {
        bool[] _isParamArray;
        int _startIndex = -1;

        internal string Name { get; set; }
        internal ICode NameCode { get; set; }
        internal bool VanishIfEmptyParams { get; set; }

        internal IEnumerable<ICode> InitAndConvertArguments(MethodCallExpression expression, ExpressionConverter converter)
        {
            lock (this)
            {
                if (string.IsNullOrEmpty(Name)) Name = expression.Method.Name.ToUpper();

                if (NameCode == null) NameCode = Name.ToCode();

                if (_startIndex == -1) _startIndex = expression.SkipMethodChain(0);

                if (_isParamArray == null)
                {
                    var paramsInfo = expression.Method.GetParameters();
                    _isParamArray = new bool[paramsInfo.Length];
                    for (int i = 0; i < paramsInfo.Length; i++)
                    {
                        _isParamArray[i] = paramsInfo[i].GetAttribute<ParamArrayAttribute>() != null;
                    }
                }
            }

            var args = new List<ICode>();
            for (int i = _startIndex; i < expression.Arguments.Count; i++)
            {
                var argExp = expression.Arguments[i];
                if (_isParamArray[i])
                {
                    var newArrayExp = argExp as NewArrayExpression;
                    if (newArrayExp != null)
                    {
                        bool isEmpty = true;
                        foreach (var e in newArrayExp.Expressions)
                        {
                            var argCode = converter.ConvertToCode(e);
                            if (isEmpty) isEmpty = argCode.IsEmpty;
                            args.Add(argCode);
                        }
                        if (VanishIfEmptyParams && isEmpty) return null;
                    }
                    else
                    {
                        var obj = converter.ConvertToObject(argExp);
                        foreach (var e in (IEnumerable)obj) args.Add(converter.ConvertToCode(e));
                    }
                }
                else
                {
                    args.Add(converter.ConvertToCode(argExp));
                }
            }
            return args;
        }
    }
}

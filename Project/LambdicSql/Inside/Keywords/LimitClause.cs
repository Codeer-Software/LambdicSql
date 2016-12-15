﻿using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.Inside.Keywords
{
    static class LimitClause
    {
        internal static TextParts Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = new HText(method.Arguments.Skip(method.SkipMethodChain(0)).Select(e=>converter.Convert(e)).ToArray()) { Separator = ", " };
            return new HText("LIMIT ", args) { IsFunctional = true};
        }
    }
}

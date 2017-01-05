﻿namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    internal class SelectQuerySyntax : Syntax
    {
        Syntax _core;

        internal SelectQuerySyntax(Syntax core)
        {
            _core = core;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
        {
            var target = isTopLevel ? _core : _core.ConcatAround("(", ")");
            return target.ToString(false, indent, context);
        }

        public override Syntax ConcatAround(string front, string back) => new SelectQuerySyntax(_core.ConcatAround(front, back));

        public override Syntax ConcatToFront(string front) => new SelectQuerySyntax(_core.ConcatToFront(front));

        public override Syntax ConcatToBack(string back) => new SelectQuerySyntax(_core.ConcatToBack(back));

        public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}

using LambdicSql.BuilderServices.Inside;
using System;
using System.Collections.Generic;

namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Arrange the code vertically.
    /// </summary>
    public class VCode : ICode
    {
        List<ICode> _core = new List<ICode>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code">code.</param>
        public VCode(params ICode[] code)
        {
            AddRange(code);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code">code.</param>
        public VCode(IEnumerable<ICode> code)
        {
            AddRange(code);
        }

        /// <summary>
        /// Separator
        /// </summary>
        public string Separator { get; set; }

        /// <summary>
        /// Indent
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Is empty.
        /// </summary>
        public bool IsEmpty => _core.Count == 0;

        /// <summary>
        /// Is single line.
        /// </summary>
        public bool IsSingleLine(BuildingContext context)
        {
            switch (_core.Count)
            {
                case 0: return true;
                case 1: return _core[0].IsSingleLine(context);
                default: return false;
            }
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        public string ToString(BuildingContext context)
        {
            var next = context.ChangeIndent(context.Indent + Indent);

            var code = new string[_core.Count];
            for (int i = 0; i < code.Length; i++)
            {
                code[i] = _core[i].ToString(next).TrimEnd();
            }
            
            return PartsUtils.Join(Separator + Environment.NewLine, code);
        }
        
        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Add(ICode text)
        {
            if (text.IsEmpty) return;
            _core.Add(text);
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(IEnumerable<ICode> texts)
        {
            foreach (var e in texts)
            {
                if (!e.IsEmpty)
                {
                    _core.Add(e);
                }
            }
        }

        /// <summary>
        /// Add text.
        /// </summary>
        /// <param name="texts">Texts.</param>
        public void AddRange(params ICode[] texts)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                if (!texts[i].IsEmpty)
                {
                    _core.Add(texts[i]);
                }
            }
        }

        /// <summary>
        /// Accept customizer.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Destination.</returns>
        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;

            var hDst = new VCode() { Indent = Indent, Separator = Separator };
            foreach (var e in _core)
            {
                hDst._core.Add(e.Accept(customizer));
            }
            return hDst;
        }
    }
}

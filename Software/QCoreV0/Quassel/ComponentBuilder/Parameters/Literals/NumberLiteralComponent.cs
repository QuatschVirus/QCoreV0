using QCoreV0.Common.Values;
using QCoreV0.Quassel.Core;
using QCoreV0.Quassel.Parsing.AST;
using QCoreV0.Quassel.Parsing.AST.Nodes;
using QCoreV0.Quassel.Parsing.AST.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.ComponentBuilder.Parameters.Literals
{
	public class NumberLiteralComponent : ParameterComponent
	{
		protected NumberValue value;
		protected int? width = null;
		protected bool? isSigned = null;

		internal NumberLiteralComponent(NumberLiteralSyntaxToken element, CodeSyntaxNode ast, QuasselManager qm) : base(element, ast, qm)
		{
			if (!NumberValue.TryGetFromLiteral(element.Content, out value)) throw new ArgumentException("Invalid number literal", nameof(element));
		}

		public override bool MachineCodeConvertable => true;

		public override ulong ComputeMachineCode()
		{
			if (!width.HasValue || !isSigned.HasValue) throw new InvalidOperationException("Width and signedness must be set before computing machine code.");
			if (value.GetRequiredWidth() > width.Value)
				throw new InvalidOperationException($"The value's required width ({value.GetRequiredWidth()}) exceeds the specified width ({width.Value}).");
			return value.GetBitValue(width!.Value, isSigned!.Value);
		}

		public override bool Verify()
		{
			return width.HasValue && isSigned.HasValue && value.GetRequiredWidth() <= width.Value;
		}

		internal void SetSigned(bool signed)
		{
			isSigned = signed;
		}

		internal void SetWidth(int width)
		{
			if (width < 1 || width > 64) throw new ArgumentOutOfRangeException(nameof(width), "Width must be between 1 and 64 bits.");
			this.width = width;
		}
	}
}

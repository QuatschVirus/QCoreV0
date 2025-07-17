using QCoreV0.Common.Addressing;
using QCoreV0.Common.Util;
using QCoreV0.Quassel.Core;
using QCoreV0.Quassel.Parsing.AST;
using QCoreV0.Quassel.Parsing.AST.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.ComponentBuilder.Containers
{
	public abstract class SectionComponent : Component
	{
		internal SectionComponent(ContainerStartSyntaxNode element, CodeSyntaxNode ast, QuasselManager qm) : base(element, ast, qm)
		{
			ParameterListSyntaxNode parameterList = element.GetChild<ParameterListSyntaxNode>() ?? throw new ArgumentException("Sections require a parameter list for at least the type");
			List<ParameterSyntaxNode> parameters = [.. parameterList.GetChildren<ParameterSyntaxNode>().Skip(FirstParametersToSkip)];
		}

		/// <summary>
		/// How many parameters to skip before the first parameter that can be read by the SectionComponent constructor (adress, alignment, min size, max size).
		/// Parameters with an identifier are ignored
		/// </summary>
		protected abstract int FirstParametersToSkip { get; }
		protected abstract byte DefaultAlignment { get; }

		public MemoryAddress? RequestedAddress { get; }
		public byte? RequestedAlignment { get; }
		public (int, int)? RequestedSizeRange { get; }

		private MemoryAddress? setAddress;
		private byte? setAlignment;
		private (int min, int max)? setSizeRange;

		public MemoryAddress? Address => setAddress ?? RequestedAddress;
		public byte Alignment => setAlignment ?? RequestedAlignment ?? DefaultAlignment;
		public (int min, int max) SizeRange => setSizeRange ?? RequestedSizeRange ?? (0, int.MaxValue);

		public abstract int GetRawSize();

		public int GetAlignedSize()
		{
			return GetRawSize().Align(Alignment);
		}

		public bool SizeFitsRange()
		{
			var size = GetAlignedSize();
			var (min, max) = SizeRange;
			return min <= size && size <= max;
		}

		public void SetAddress(MemoryAddress address)
		{
			setAddress = address;
		}

		public void SetAlignment(byte alignment)
		{
			setAlignment = alignment;
		}

		public void SetSizeRange(int min, int max)
		{
			if (min < 0 || max < 0 || min > max)
			{
				throw new ArgumentOutOfRangeException(nameof(min), "Size range must be non-negative and min must not exceed max.");
			}
			setSizeRange = (min, max);
		}
	}
}

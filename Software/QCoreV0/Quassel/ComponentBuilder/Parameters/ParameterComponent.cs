using QCoreV0.Quassel.ComponentBuilder.Parameters.RegisterReferences;
using QCoreV0.Quassel.Core;
using QCoreV0.Quassel.Parsing.AST;
using QCoreV0.Quassel.Parsing.AST.Nodes;
using QCoreV0.Quassel.Parsing.AST.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.ComponentBuilder.Parameters
{
	public abstract class ParameterComponent : Component
	{
		internal ParameterComponent(SyntaxElement element, CodeSyntaxNode ast, QuasselManager qm) : base(element, ast, qm)
		{
		}

		public abstract bool MachineCodeConvertable { get; }

		public abstract ulong ComputeMachineCode();

		public static ParameterComponent CreateParameterComponent(ParameterSyntaxNode element, CodeSyntaxNode ast)
		{
			var pvsn = element.GetChild<ParameterValueSyntaxNode>()!;

			if (pvsn.TryGetChild<RegisterReferenceSyntaxNode>(out var regref))
			{
				return regref!.GetChild<SyntaxToken>() switch
				{
					GeneralPurposeRegisterReferenceSyntaxToken gprrst => new GeneralPurposeRegisterReferenceComponent(gprrst, ast),
					_ => throw new NotSupportedException($"Register reference type '{regref.GetChild<SyntaxToken>()?.GetType().Name}' is not supported.")
				};
			} else
			{
				return pvsn.GetChild<SyntaxToken>() switch
				{
					_ => throw new NotSupportedException($"Parameter value type '{pvsn.GetChild<SyntaxToken>()?.GetType().Name}' is not supported.")
				};
			}
		}
	}
}

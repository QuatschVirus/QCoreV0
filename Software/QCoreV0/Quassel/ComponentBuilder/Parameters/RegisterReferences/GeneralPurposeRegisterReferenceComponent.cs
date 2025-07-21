using QCoreV0.Quassel.Core;
using QCoreV0.Quassel.Parsing.AST;
using QCoreV0.Quassel.Parsing.AST.Nodes;
using QCoreV0.Quassel.Parsing.AST.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.ComponentBuilder.Parameters.RegisterReferences
{
	public class GeneralPurposeRegisterReferenceComponent : RegisterReferenceComponent
	{
		protected byte registerIndex;

		internal GeneralPurposeRegisterReferenceComponent(SyntaxElement element, CodeSyntaxNode ast, QuasselManager qm) : base(element, ast, qm)
		{
			registerIndex = byte.Parse(element.Content[1..]);
		}

		public override ulong ComputeMachineCode()
		{
			return (ulong)(registerIndex & 0x1F); 
		}

		public override bool Verify()
		{
			return registerIndex < 32;
		}
	}
}

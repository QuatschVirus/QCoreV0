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
	public abstract class RegisterReferenceComponent : ParameterComponent
	{
		internal RegisterReferenceComponent(SyntaxElement element, CodeSyntaxNode ast) : base(element, ast)
		{
		}

		public override bool MachineCodeConvertable => true;
	}
}

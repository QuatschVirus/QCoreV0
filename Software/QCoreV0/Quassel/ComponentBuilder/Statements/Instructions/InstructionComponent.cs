using QCoreV0.Quassel.Core;
using QCoreV0.Quassel.Parsing.AST.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.ComponentBuilder.Statements.Instructions
{
	public abstract class InstructionComponent : StatementComponent
	{
		internal InstructionComponent(InstructionSyntaxNode element, CodeSyntaxNode ast, QuasselManager qm) : base(element, ast, qm)
		{
		}
	}
}

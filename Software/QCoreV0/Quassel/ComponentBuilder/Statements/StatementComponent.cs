using QCoreV0.Quassel.Core;
using QCoreV0.Quassel.Parsing.AST;
using QCoreV0.Quassel.Parsing.AST.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.ComponentBuilder.Statements
{
	public abstract class StatementComponent : Component
	{
		internal StatementComponent(SyntaxElement element, CodeSyntaxNode ast, QuasselManager qm) : base(element, ast, qm)
		{
		}
	}
}

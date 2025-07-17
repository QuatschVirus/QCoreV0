using QCoreV0.Quassel.Core;
using QCoreV0.Quassel.Parsing.AST;
using QCoreV0.Quassel.Parsing.AST.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.ComponentBuilder
{
	public abstract class Component
	{
		public SyntaxElement Element { get; private set; }
		public CodeSyntaxNode AST { get; private set; }
		public QuasselManager QuasselManager { get; private set; }
		internal Component(SyntaxElement element, CodeSyntaxNode ast, QuasselManager qm)
		{
			ArgumentNullException.ThrowIfNull(element, nameof(element));
			ArgumentNullException.ThrowIfNull(ast, nameof(ast));
			ArgumentNullException.ThrowIfNull(qm, nameof(qm));
			Element = element;
			AST = ast;
			QuasselManager = qm;
		}
	}
}

using Antlr4.Runtime;
using QCoreV0.Common.Util;
using QCoreV0.Quassel.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.Parsing.AST.Nodes
{
	public abstract class SyntaxNode : SyntaxElement
	{
		internal SyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}

		internal SyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx.GetText(), ctx.GetStart(), children)
		{
			ArgumentNullException.ThrowIfNull(ctx);
			ArgumentNullException.ThrowIfNull(children);
		}
	}

	public class CodeSyntaxNode : SyntaxNode
	{
		internal CodeSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
		}

		internal CodeSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
	}

	public class CodeLineSyntaxNode : SyntaxNode
	{
		internal CodeLineSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal CodeLineSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
			ArgumentNullException.ThrowIfNull(ctx);
			ArgumentNullException.ThrowIfNull(children);
		}
	}

	public class StatementSyntaxNode : SyntaxNode
	{
		internal StatementSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}

		internal StatementSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
		}
	}

	public class DirectiveSyntaxNode : SyntaxNode
	{
		internal DirectiveSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}

		internal DirectiveSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
			ArgumentNullException.ThrowIfNull(ctx);
			ArgumentNullException.ThrowIfNull(children);
		}
	}

	public class InstructionSyntaxNode : SyntaxNode
	{
		internal InstructionSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}

		internal InstructionSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
			ArgumentNullException.ThrowIfNull(ctx);
			ArgumentNullException.ThrowIfNull(children);
		}
	}

	public class ContainerStartSyntaxNode : SyntaxNode
	{
		internal ContainerStartSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal ContainerStartSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
			ArgumentNullException.ThrowIfNull(ctx);
			ArgumentNullException.ThrowIfNull(children);
		}
	}

	public class ContainerEndSyntaxNode : SyntaxNode
	{
		internal ContainerEndSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal ContainerEndSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
			ArgumentNullException.ThrowIfNull(ctx);
			ArgumentNullException.ThrowIfNull(children);
		}
	}

	public class ParameterListSyntaxNode : SyntaxNode
	{
		internal ParameterListSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}

		internal ParameterListSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
			ArgumentNullException.ThrowIfNull(ctx);
			ArgumentNullException.ThrowIfNull(children);
		}
	}

	public class ParameterSyntaxNode : SyntaxNode
	{
		internal ParameterSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}

		internal ParameterSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
			ArgumentNullException.ThrowIfNull(ctx);
			ArgumentNullException.ThrowIfNull(children);
		}
	}

	public class ParameterIdentifierSyntaxNode : SyntaxNode
	{
		internal ParameterIdentifierSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal ParameterIdentifierSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
			ArgumentNullException.ThrowIfNull(ctx);
			ArgumentNullException.ThrowIfNull(children);
		}
	}

	public class ParameterValueSyntaxNode : SyntaxNode
	{
		internal ParameterValueSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal ParameterValueSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
			ArgumentNullException.ThrowIfNull(ctx);
			ArgumentNullException.ThrowIfNull(children);
		}
	}

	public class RegisterReferenceSyntaxNode : SyntaxNode
	{
		internal RegisterReferenceSyntaxNode(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal RegisterReferenceSyntaxNode(ParserRuleContext ctx, List<SyntaxElement> children) : base(ctx, children)
		{
			ArgumentNullException.ThrowIfNull(ctx);
			ArgumentNullException.ThrowIfNull(children);
		}
	}
}

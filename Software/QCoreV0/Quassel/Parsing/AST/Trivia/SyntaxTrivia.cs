using Antlr4.Runtime;
using QCoreV0.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.Parsing.AST.Trivia
{
	public class SyntaxTrivia : SyntaxElement
	{
		internal SyntaxTrivia(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}

		internal SyntaxTrivia(IToken token) : base(token.Text, new Location(token.Line, token.Column), [])
		{
		}
	}

	public class CommaSyntaxTrivia : SyntaxTrivia
	{
		internal CommaSyntaxTrivia(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal CommaSyntaxTrivia(IToken token) : base(token)
		{
		}
	}

	public class SemicolonSyntaxTrivia : SyntaxTrivia
	{
		internal SemicolonSyntaxTrivia(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal SemicolonSyntaxTrivia(IToken token) : base(token)
		{
		}
	}

	public class DotSyntaxTrivia : SyntaxTrivia
	{
		internal DotSyntaxTrivia(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal DotSyntaxTrivia(IToken token) : base(token)
		{
		}
	}

	public class HashSyntaxTrivia : SyntaxTrivia
	{
		internal HashSyntaxTrivia(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}

		internal HashSyntaxTrivia(IToken token) : base(token)
		{
		}
	}

	public class ColonSyntaxTrivia : SyntaxTrivia
	{
		internal ColonSyntaxTrivia(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal ColonSyntaxTrivia(IToken token) : base(token)
		{
		}
	}

	public class SpaceSyntaxTrivia : SyntaxTrivia
	{
		internal SpaceSyntaxTrivia(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal SpaceSyntaxTrivia(IToken token) : base(token)
		{
		}
	}

	public class NewLineSyntaxTrivia : SyntaxTrivia
	{
		internal NewLineSyntaxTrivia(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal NewLineSyntaxTrivia(IToken token) : base(token)
		{
		}
	}

	public class EndOfFileSyntaxTrivia : SyntaxTrivia
	{
		internal EndOfFileSyntaxTrivia(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal EndOfFileSyntaxTrivia(IToken token) : base(token)
		{
			if (token.Type != TokenConstants.EOF)
			{
				throw new ArgumentException("Token must be of type EOF.", nameof(token));
			}
		}
		protected override string RenderContent()
		{
			return "<EOF>";
		}
	}
}

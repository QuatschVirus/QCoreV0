using Antlr4.Runtime;
using QCoreV0.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.Parsing.AST.Tokens
{
	public class SyntaxToken : SyntaxElement
	{
		internal SyntaxToken(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}

		internal SyntaxToken(IToken token) : base(token.Text, new Location(token.Line, token.Column), [])
		{
		}
	}

	public class IdentifierSyntaxToken : SyntaxToken
	{
		internal IdentifierSyntaxToken(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal IdentifierSyntaxToken(IToken token) : base(token)
		{
		}
	}

	public class CommentSyntaxToken : SyntaxToken
	{
		internal CommentSyntaxToken(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal CommentSyntaxToken(IToken token) : base(token)
		{
		}
	}

	public class BlockCommentSyntaxToken : SyntaxToken
	{
		internal BlockCommentSyntaxToken(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal BlockCommentSyntaxToken(IToken token) : base(token)
		{
		}
	}

	public class StringLiteralSyntaxToken : SyntaxToken
	{
		internal StringLiteralSyntaxToken(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal StringLiteralSyntaxToken(IToken token) : base(token)
		{
		}
	}

	public class NumberLiteralSyntaxToken : SyntaxToken
	{
		internal NumberLiteralSyntaxToken(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal NumberLiteralSyntaxToken(IToken token) : base(token)
		{
		}
	}

	public class CharacterLiteralSyntaxToken : SyntaxToken
	{
		internal CharacterLiteralSyntaxToken(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal CharacterLiteralSyntaxToken(IToken token) : base(token)
		{
		}
	}

	public class GeneralPurposeRegisterReferenceSyntaxToken : SyntaxToken
	{
		internal GeneralPurposeRegisterReferenceSyntaxToken(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal GeneralPurposeRegisterReferenceSyntaxToken(IToken token) : base(token)
		{
		}
	}

	public class ControlStatusRegisterReferenceSyntaxToken : SyntaxToken
	{
		internal ControlStatusRegisterReferenceSyntaxToken(string content, Location start, List<SyntaxElement> children) : base(content, start, children)
		{
		}
		internal ControlStatusRegisterReferenceSyntaxToken(IToken token) : base(token)
		{
		}
	}
}

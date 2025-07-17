using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using QCoreV0.Common.Util;
using QCoreV0.Quassel.Parsing.AST;
using QCoreV0.Quassel.Parsing.AST.Nodes;
using QCoreV0.Quassel.Parsing.AST.Tokens;
using QCoreV0.Quassel.Parsing.AST.Trivia;
using QCoreV0.Quassel.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QCoreV0.Quassel.Parsing
{
	internal class ASTConversionVisitor : qcalBaseVisitor<SyntaxElement>
	{
		public override SyntaxElement VisitDirective([NotNull] qcalParser.DirectiveContext context)
		{
			return new DirectiveSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitInstruction([NotNull] qcalParser.InstructionContext context)
		{
			return new InstructionSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitParameter([NotNull] qcalParser.ParameterContext context)
		{
			return new ParameterSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitParameter_list([NotNull] qcalParser.Parameter_listContext context)
		{
			return new ParameterListSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitCode([NotNull] qcalParser.CodeContext context)
		{
			return new CodeSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitCode_line([NotNull] qcalParser.Code_lineContext context)
		{
			return new CodeLineSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitRegref([NotNull] qcalParser.RegrefContext context)
		{
			return new RegisterReferenceSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitStatement([NotNull] qcalParser.StatementContext context)
		{
			return new StatementSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitContainer_start([NotNull] qcalParser.Container_startContext context)
		{
			return new ContainerStartSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitContainer_end([NotNull] qcalParser.Container_endContext context)
		{
			return new ContainerEndSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitParameter_identifier([NotNull] qcalParser.Parameter_identifierContext context)
		{
			return new ParameterIdentifierSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitParameter_value([NotNull] qcalParser.Parameter_valueContext context)
		{
			return new ParameterValueSyntaxNode(context, [.. context.children.Select(Visit)]);
		}

		public override SyntaxElement VisitTerminal(ITerminalNode node)
		{
			Location start = new(node.Symbol.Line, node.Symbol.Column);
			string content = node.GetText();
			return node.Symbol.Type switch
			{
				qcalParser.COMMENT => new CommentSyntaxToken(node.Symbol),
				qcalParser.BLOCKCOMMENT => new BlockCommentSyntaxToken(node.Symbol),
				qcalParser.GPREGREF => new GeneralPurposeRegisterReferenceSyntaxToken(node.Symbol),
				qcalParser.CSREGREF => new ControlStatusRegisterReferenceSyntaxToken(node.Symbol),
				qcalParser.IDENTIFIER => new IdentifierSyntaxToken(node.Symbol),
				qcalParser.STRINGLITERAL => new StringLiteralSyntaxToken(node.Symbol),
				qcalParser.NUMLITERAL => new NumberLiteralSyntaxToken(node.Symbol),
				qcalParser.CHARLITERAL => new CharacterLiteralSyntaxToken(node.Symbol),

				qcalParser.COMMA => new CommaSyntaxTrivia(node.Symbol),
				qcalParser.SEMICOLON => new SemicolonSyntaxTrivia(node.Symbol),
				qcalParser.DOT => new DotSyntaxTrivia(node.Symbol),
				qcalParser.HASH => new HashSyntaxTrivia(node.Symbol),
				qcalParser.COLON => new ColonSyntaxTrivia(node.Symbol),
				qcalParser.SPACE => new SpaceSyntaxTrivia(node.Symbol),
				qcalParser.NEWLINE => new NewLineSyntaxTrivia(node.Symbol),

				TokenConstants.EOF => new EndOfFileSyntaxTrivia(node.Symbol),

				_ => throw new ArgumentException($"'{content}' is not a valid terminal node (type : {node.Symbol.Type}).", nameof(node))
			};
		}

		public override SyntaxElement VisitErrorNode(IErrorNode node)
		{
			Console.WriteLine($"Error node encountered: {node.GetText()} at {node.Symbol.Line}:{node.Symbol.Column}");
			return base.VisitErrorNode(node);
		}

		public override SyntaxElement Visit(IParseTree tree)
		{
#if DEBUG
			Console.WriteLine($"Visiting: {tree.GetType().Name}: '{tree.GetText().Replace("\n", @"\n").Replace("\r", @"\r")}'");
#endif
			return base.Visit(tree);
		}
	}
}

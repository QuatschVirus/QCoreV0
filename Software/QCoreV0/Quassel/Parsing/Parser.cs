using Antlr4.Runtime;
using QCoreV0.Quassel.Core;
using QCoreV0.Quassel.Parsing.AST;
using QCoreV0.Quassel.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.Parsing
{
	public class Parser
	{
		protected QuasselManager qm;

		internal Parser(QuasselManager quasselManager)
		{
			this.qm = quasselManager;
		}

		public SyntaxElement Parse(string content)
		{
			if (string.IsNullOrWhiteSpace(content))
			{
				throw new ArgumentException("Content cannot be null or empty.", nameof(content));
			}

			qcalLexer lexer = new(new AntlrInputStream(content));
			CommonTokenStream tokens = new(lexer);
			tokens.Fill();

			Console.WriteLine("Tokens:\n" + string.Join(Environment.NewLine, tokens.GetTokens().Select(t => $"{lexer.Vocabulary.GetSymbolicName(t.Type)}: {t.Text}")));

			qcalParser parser = new(tokens);
			var program = parser.code();

			ASTConversionVisitor visitor = new();
			return visitor.Visit(program);
		}
	}
}

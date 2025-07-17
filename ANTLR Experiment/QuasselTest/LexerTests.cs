using Antlr4.Runtime;

namespace QuasselTest
{
	[TestClass]
	public sealed class LexerTests
	{
		[TestMethod]
		public void Lexer_Fibonacci()
		{
			// Arrange
			var input = File.ReadAllText("./QCAL/Fibonacci.qcal");
			var lexer = new qcalLexer(new AntlrInputStream(input));

			var tokens = new CommonTokenStream(lexer);
			tokens.Fill();

			Assert.AreEqual(46, tokens.Size, "Differing token count, should be 45, found " + tokens.Size);
		}
	}
}

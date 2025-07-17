using QCoreV0.Quassel.Core;
using QCoreV0.Quassel.Projects;

namespace QCoreV0.Quassel.Tests;

[TestClass]
public class ParsingTests
{
    [TestMethod]
    public void FibonacciTest()
    {
		// Arrange
        var qm = new QuasselManager();
        qm.ChangeProject(Project.CreateTemp());

        var parser = qm.GetParser();
        string content = File.ReadAllText("QCAL/Fibonacci.qcal");

		// Act
        var syntaxTree = parser.Parse(content);
        Console.WriteLine("AST for Fibonacci.qcal:");
        Console.WriteLine(syntaxTree.Render());
	}
}

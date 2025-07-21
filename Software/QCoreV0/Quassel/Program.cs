using QCoreV0.Common.Util;
using QCoreV0.Quassel.Projects;
using System.CommandLine;

namespace QCoreV0.Quassel
{
	internal class Program
	{
		private static readonly RootCommand rt = new("Quassel is the dedicated first-party assembler for the QCreV0 and QCAL.");

		static void Main(string[] args)
		{


			rt.Invoke(args);
		}
	}
}

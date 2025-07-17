using QCoreV0.Common.Util;
using QCoreV0.Quassel.CLI;
using QCoreV0.Quassel.Manifests;
using QCoreV0.Quassel.Projects;
using System.CommandLine;

namespace QCoreV0.Quassel
{
	internal class Program
	{
		private static readonly RootCommand rt = new("Quassel is the dedicated first-party assmebler for the QCoreV0 and QCAL.");

		static void Main(string[] args)
		{

			ProjectCommand projectCommand = new(rt);

			rt.Invoke(args);
		}
	}
}

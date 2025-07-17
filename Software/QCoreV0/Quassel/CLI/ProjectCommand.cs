using QCoreV0.Quassel.Manifests;
using QCoreV0.Quassel.Projects;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.CLI
{
	public class ProjectCommand
	{
		private readonly RootCommand rootCommand;
		private readonly Command mainCommand = new("project", "Manages a QCAL project");

		private readonly Command createCommand = new("create", "Create a new QCAL project with a default manifest file");
		private readonly Argument<FileInfo> manifestFileOption = new("manifest-file", () => new(Manifest.DefaultManifestFileName), "The path to the manifest file for the project");

		public ProjectCommand(RootCommand rootCommand)
		{
			this.rootCommand = rootCommand;

			createCommand.AddArgument(manifestFileOption);
			createCommand.SetHandler(Create, manifestFileOption);
			mainCommand.AddCommand(createCommand);

			rootCommand.AddCommand(mainCommand);
		}

		private void Create(FileInfo manifestFile)
		{
			if (!Manifest.PathIsCandidate(manifestFile.FullName))
			{
				throw new ArgumentException($"The file '{manifestFile.FullName}' is not a valid manifest file. It must have the '.qcam' extension.");
			}
			Project project = Project.CreateNew(manifestFile);
			project.Save();
		}
	}
}

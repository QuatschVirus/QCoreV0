using QCoreV0.Quassel.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.Projects
{
	public class Project
	{
		public Config Config { get; private set; }
		public FileInfo? ProjectFile { get; private set; }

		public Project(FileInfo projectFile, string? globalFile = "global.json")
		{
			ProjectFile = projectFile;
			Config = new Config(projectFile.FullName, globalFile);
		}

		public Project(string? globalFile = "global.json")
		{
			Config = new Config(null, globalFile);
		}

		public static Project CreateTemp()
		{
			return new Project();
		}
	}
}

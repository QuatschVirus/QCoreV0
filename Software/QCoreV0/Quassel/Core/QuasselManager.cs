using QCoreV0.Quassel.Configs;
using QCoreV0.Quassel.Parsing;
using QCoreV0.Quassel.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.Core
{
	public class QuasselManager
	{
		public Project? ActiveProject { get; private set; }
		public Config? Config => this.ActiveProject?.Config;

		public QuasselManager()
		{

		}

		public void ChangeProject(Project newProject)
		{
			this.ActiveProject = newProject;
		}

		public Parser GetParser()
		{
			if (this.ActiveProject == null)
			{
				throw new InvalidOperationException("No active project set.");
			}
			return new Parser(this);
		}
	}
}

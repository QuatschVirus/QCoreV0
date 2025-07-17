using QCoreV0.Quassel.Configs;
using QCoreV0.Quassel.Manifests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.Projects
{
	public class Project
	{
		public Manifest Manifest { get; private set; }
		public FileInfo? ManifestFile { get; private set; }

		public bool IsTemporary => this.ManifestFile == null;

		public Config Config { get; private set; }

		public Project(FileInfo manifestFile)
		{
			this.Manifest = Manifest.Load(manifestFile);
			this.ManifestFile = manifestFile;

			var bcfg = BuildConfig.LoadFromDefaults();
			bcfg.ApplyManifestOverrides(this.Manifest);
			this.Config = bcfg.Build();

		}

		private Project(FileInfo manifestFile, Manifest manifest)
		{
			this.Manifest = manifest;
			this.ManifestFile = manifestFile;

			var bcfg = BuildConfig.LoadFromDefaults();
			bcfg.ApplyManifestOverrides(this.Manifest);
			this.Config = bcfg.Build();
		}

		private Project(Manifest manifest)
		{
			this.Manifest = manifest;
			this.ManifestFile = null;
			var bcfg = BuildConfig.LoadFromDefaults();
			bcfg.ApplyManifestOverrides(this.Manifest);
			this.Config = bcfg.Build();
		}

		public static Project CreateNew(FileInfo manifestFile)
		{
			var manifest = Manifest.CreateEmpty();
			return new Project(manifestFile, manifest);
		}

		public static Project CreateTemp()
		{
			return new Project(Manifest.CreateEmpty());
		}

		protected bool IsSourceFileIncluded(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				return false;
			}

			if (!Path.IsPathRooted(path))
			{
				path = Path.GetFullPath(path);
			}

			var srcs = Manifest.Sources.Select(src => Path.GetFullPath(src, ManifestFile?.DirectoryName ?? Directory.GetCurrentDirectory())).ToList();

			return srcs.Any(path.StartsWith);
		}

		public void Save()
		{
			if (this.ManifestFile != null)
			{
				File.WriteAllText(ManifestFile.FullName, Tomlyn.Toml.FromModel(Manifest));
			}
		}
	}
}

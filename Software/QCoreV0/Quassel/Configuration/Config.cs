using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Quassel.Configs
{
	public class Config
	{
		public const string DefaultsFileName = "defaults.json";

		protected IConfiguration config;

		public Config(string? projectFile, string? globalFile = "global.json")
		{
			string executablePath = AppContext.BaseDirectory;

			var builder = new ConfigurationBuilder()
				.AddJsonFile(Path.Combine(executablePath, DefaultsFileName), optional: false, reloadOnChange: false);

			if (!string.IsNullOrWhiteSpace(globalFile))
			{
				builder.AddJsonFile(Path.Combine(executablePath, globalFile), optional: true, reloadOnChange: false);
			}

			if (!string.IsNullOrWhiteSpace(projectFile))
			{
				builder.AddJsonFile(Path.GetFullPath(projectFile), optional: true, reloadOnChange: false);
			}

			builder.AddEnvironmentVariables(prefix: "QUASSEL_");

			config = builder.Build();
		}

		public T? GetValue<T>(string key, T? defaultValue = default)
		{
			return config.GetValue(key.Replace('.', ':'), defaultValue);
		}

		public T? GetBound<T>(string sectionName) where T : class, new()
		{
			var section = config.GetSection(sectionName.Replace('.', ':'));
			if (section.Exists())
			{
				return section.Get<T>();
			}
			return null;
		}

		public string this[string key]
		{
			get => config[key.Replace('.', ':')] ?? throw new KeyNotFoundException($"Key '{key}' not found in configuration.");
			set => config[key.Replace('.', ':')] = value;
		}

		public IConfiguration GetConfiguration()
		{
			return config;
		}
	}
}

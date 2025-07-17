using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QCoreV0.Common.Util
{
	public static class RegexUtil
	{
		public static (long line, int column) GetLineAndColumn(string content, int position)
		{
			if (string.IsNullOrEmpty(content) || position < 0 || position >= content.Length)
				throw new ArgumentOutOfRangeException(nameof(position), $"Position {position} is out of range.");

			long line = content[..position].LongCount(chr => chr == '\n') + 1;
			int column = position - content.LastIndexOf('\n', position);

			return (line, column);
		}

		public static (long line, int column) GetLineAndColumn(this Match match, string content)
		{
			return GetLineAndColumn(content, match.Index);
		}

		public static (long line, int column) GetLineAndColumn(this Group group, string content)
		{
			return GetLineAndColumn(content, group.Index);
		}
	}
}

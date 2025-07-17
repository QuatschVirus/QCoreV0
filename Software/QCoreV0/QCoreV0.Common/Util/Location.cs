using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Common.Util
{
	public readonly struct Location(int line, int column)
	{
		public int Line { get; } = line;
		public int Column { get; } = column;

		public override string ToString()
		{
			return $"{Line}:{Column}";
		}

		public Location ComputeEnd(string text)
		{
			ArgumentNullException.ThrowIfNull(text);
			int endLine = Line;
			int endColumn = Column + text.Length;
			// Adjust for line breaks
			int lineBreaks = text.Count(c => c == '\n');
			if (lineBreaks > 0)
			{
				endLine += lineBreaks;
				endColumn = text.LastIndexOf('\n') == -1 ? endColumn : text.Length - text.LastIndexOf('\n') - 1;
			}
			return new Location(endLine, endColumn);
		}
	}
}

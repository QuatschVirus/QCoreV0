using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Common.Util
{
	public static class StringUtil
	{
		public static string Truncate(this string str, int maxLength, string suffix = "...")
		{
			if (string.IsNullOrEmpty(str) || maxLength < 0)
				return str;
			if (str.Length <= maxLength)
				return str;
			if (maxLength <= suffix.Length)
				return str[..maxLength];
			return str[..(maxLength - suffix.Length)] + suffix;
		}

		public static (char highestLower, char highestUpper) AnalyzeCharRange(string str)
		{
			if (string.IsNullOrEmpty(str))
				throw new ArgumentException("Input string cannot be null or empty.", nameof(str));
			char highestLower = char.MinValue;
			char highestUpper = char.MinValue;
			foreach (char c in str)
			{
				if (char.IsLower(c) && c > highestLower)
					highestLower = c;
				else if (char.IsUpper(c) && c > highestUpper)
					highestUpper = c;
			}
			return (highestLower, highestUpper);
		}
	}
}

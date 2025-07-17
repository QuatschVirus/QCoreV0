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
	}
}

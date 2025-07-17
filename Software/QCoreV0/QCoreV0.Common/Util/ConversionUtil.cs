using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Common.Util
{
	public static class ConversionUtil
	{
		public static bool TryGetFromBase64(string str, out byte[] bytes)
		{
			if (string.IsNullOrWhiteSpace(str))
			{
				bytes = [];
				return false;
			}
			try
			{
				bytes = FromBase64(str);
				return true;
			}
			catch (FormatException)
			{
				bytes = [];
				return false;
			}
		}

		public static byte[] FromBase64(string str)
		{
			if (string.IsNullOrWhiteSpace(str))
				throw new ArgumentNullException(nameof(str), "Input string cannot be null or empty.");
			str = str.Trim();
			if (str.Contains('_') || str.Contains('-')) // URL-safe Base64
			{
				str = str.Replace('_', '/').Replace('-', '+');
			}
			if (str.Length % 4 != 0) // Base64 padding
			{
				str = str.PadRight(str.Length + (4 - str.Length % 4), '=');
			}
			return Convert.FromBase64String(str);
		}

		public static string ToBase64(byte[] bytes, bool urlSafe = false)
		{
			if (bytes == null || bytes.Length == 0)
				throw new ArgumentNullException(nameof(bytes), "Input byte array cannot be null or empty.");
			string base64 = Convert.ToBase64String(bytes);
			if (urlSafe)
			{
				base64 = base64.Replace('+', '-').Replace('/', '_'); // URL-safe Base64
			}
			return base64;
		}
	}
}

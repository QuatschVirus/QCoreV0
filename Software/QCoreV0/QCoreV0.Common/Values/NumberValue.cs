using QCoreV0.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Common.Values
{
	public readonly struct NumberValue
	{
		public readonly byte[] Bytes;
		public readonly bool IsNegative;

		public NumberValue(byte[] bytes, bool isNegative = false)
		{
			Bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));
			IsNegative = isNegative;
		}

		public ulong GetBitValue(byte width, bool allowSigned = true)
		{
			if (width < 1 || width > 64)
				throw new ArgumentOutOfRangeException(nameof(width), "Width must be between 1 and 64 bits.");

			ulong rawAbsolute = BitConverter.ToUInt64(Bytes, 0) & NumbersUtil.GetBitMask(IsNegative && allowSigned ? width - 1 : width);

			return IsNegative && allowSigned ? (ulong)-(long)rawAbsolute : rawAbsolute;
		}

		public static bool TryGetFromLiteral(string literal, out NumberValue number)
		{
			number = default;
			if (string.IsNullOrWhiteSpace(literal)) return false;
			
			literal = literal.Trim();
			bool isNegative = literal.StartsWith('-');
			if (isNegative) literal = literal[1..].Trim();

			if (literal.Contains('G') && !isNegative) // Base64, maybe URL-safe
			{
				number = new NumberValue(ConversionUtil.FromBase64(literal), isNegative);
			}
		}
	}
}

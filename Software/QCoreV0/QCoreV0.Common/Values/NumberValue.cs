using QCoreV0.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

		public ulong GetBitValue(int width, bool allowSigned = true)
		{
			if (width < 1 || width > 64)
				throw new ArgumentOutOfRangeException(nameof(width), "Width must be between 1 and 64 bits.");

			ulong rawAbsolute = BitConverter.ToUInt64(Bytes, 0) & NumbersUtil.GetBitMask(IsNegative && allowSigned ? width - 1 : width);

			return IsNegative && allowSigned ? (ulong)-(long)rawAbsolute : rawAbsolute;
		}

		public int GetRequiredWidth()
		{
			if (Bytes == null || Bytes.Length == 0) return 0;
			ulong rawAbsolute = BitConverter.ToUInt64(Bytes, 0);
			return BitOperations.Log2(rawAbsolute) + 1 + (IsNegative ? 1 : 0);
		}

		public static bool TryGetFromLiteral(string literal, out NumberValue number)
		{
			number = default;
			if (string.IsNullOrWhiteSpace(literal)) return false;
			
			literal = literal.Trim();
			bool isNegative = literal.StartsWith('-');
			if (isNegative) literal = literal[1..].Trim();

			if (literal.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
			{
				literal = literal[2..].Trim();
				if (string.IsNullOrEmpty(literal)) return false;
				try
				{
					number = new NumberValue(Convert.FromHexString(literal), isNegative);
					return true;
				}
				catch (FormatException)
				{
					return false;
				}
			}
			else if (literal.StartsWith("0o", StringComparison.OrdinalIgnoreCase))
			{
				literal = literal[2..].Trim();
				if (string.IsNullOrEmpty(literal)) return false;

				try
				{
					number = new NumberValue(Convert.ToUInt64(literal, 8).GetBytes(), isNegative);
					return true;
				}
				catch (FormatException)
				{
					return false;
				}
			} else if (literal.StartsWith("0b", StringComparison.OrdinalIgnoreCase))
			{
				literal = literal[2..].Trim();
				if (string.IsNullOrEmpty(literal)) return false;

				try
				{
					number = new NumberValue(Convert.ToUInt64(literal, 2).GetBytes(), isNegative);
					return true;
				}
				catch (FormatException)
				{
					return false;
				}
			}
			else
			{
				if (string.IsNullOrEmpty(literal)) return false;

				try
				{
					number = new NumberValue(Convert.ToUInt64(literal).GetBytes(), isNegative);
					return true;
				}
				catch (FormatException)
				{
					return false;
				}
			}
		}
	}
}

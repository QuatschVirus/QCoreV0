using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Common.Util
{
	public static class NumbersUtil
	{
		public static int Align(this int value, int alignment)
		{
			if (alignment <= 0)
				throw new ArgumentOutOfRangeException(nameof(alignment), "Alignment must be greater than zero.");
			return ((value + alignment - 1) / alignment) * alignment;
		}

		public static ulong GetBitMask(int width)
		{
			if (width < 1 || width > 64)
				throw new ArgumentOutOfRangeException(nameof(width), "Width must be between 1 and 64 bits.");
			return (1UL << width) - 1;
		}
	}
}

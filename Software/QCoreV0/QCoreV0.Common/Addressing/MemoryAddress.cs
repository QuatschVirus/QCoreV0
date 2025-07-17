using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Common.Addressing
{
	public readonly struct MemoryAddress
	{
		public ushort Page { get; }
		public ushort Index { get; }

		public MemoryAddress(ushort page, ushort index)
		{
			Page = page;
			Index = index;
		}

		public MemoryAddress(uint combined)
		{
			Page = (ushort)(combined >> 16);
			Index = (ushort)(combined & 0xFFFF);
		}

		public override string ToString()
		{
			return $"{Page:X4}:{Index:X4}";
		}


		public static implicit operator MemoryAddress(uint combined)
		{
			return new MemoryAddress(combined);
		}

		public static implicit operator uint(MemoryAddress address)
		{
			return ((uint)address.Page << 16) | address.Index;
		}
	}
}

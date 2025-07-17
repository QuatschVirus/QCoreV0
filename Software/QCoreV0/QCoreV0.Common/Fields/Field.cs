using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Common.Fields
{
	public class Field
	{
		public string Name { get; private set; }
		public int Index { get; private set; }
		public int Width { get; private set; }

		public int? MappingStart { get; private set; }

		public int Count { get; private set; }

		public bool IsMapped
		{
			get { return MappingStart.HasValue; }
		}

		public bool SuffixCount { get; private set; }

		public bool SuffixWidth { get; private set; }

		internal Field(string name, int index, int width, int? mappingStart, int count, bool suffixCount, bool suffixWidth)
		{
			Name = name;
			Index = index;
			Width = width;
			MappingStart = mappingStart;
			Count = count;
			SuffixCount = suffixCount;
			SuffixWidth = suffixWidth;
		}

		public static string GetWidthSuffixedName(string name, int width)
		{
			return $"{name}{width}";
		}

		public string GetBase()
		{
			var @base = Name;

			if (SuffixWidth)
			{
				@base = GetWidthSuffixedName(@base, Width);
			}

			if (SuffixWidth && SuffixCount)
			{
				@base += "-";
			}

			if (SuffixCount)
			{
				@base += Count.ToString();
			}

			return @base;
		}

		public string GetKey()
		{
			var key = GetBase();
			if (IsMapped)
			{
				key += $"@{MappingStart}";
			}
			return key;
		}

		public string GetDisplay(bool reverseMapping = true)
		{
			var display = GetBase();

			if (IsMapped)
			{
				if (reverseMapping)
				{
					display += $" [{MappingStart + Width - 1}:{MappingStart}]";
				}
				else
				{
					display += $" [{MappingStart}:{MappingStart + Width - 1}]";
				}
			}

			return display;
		}

		public int GetReadMask()
		{
			if (Width <= 0)
			{
				return 0;
			}
			int mask = (int)(0xFFFF_FFFF >> (32 - Width));
			return mask << Index;
		}

		public int ComputeReadValue(int value)
		{
			if (Width <= 0 || Index < 0)
			{
				return 0;
			}
			int mask = GetReadMask();
			return (value & mask) >> Index;
		}

		public int GetWriteMask()
		{
			if (Width <= 0 || !MappingStart.HasValue)
			{
				return 0;
			}
			int mask = (int)(0xFFFF_FFFF >> (32 - Width));
			return mask << MappingStart.Value;
		}

		public int ComputeWriteValue(int value)
		{
			if (Width <= 0 || Index < 0 || !MappingStart.HasValue)
			{
				return 0;
			}
			return ComputeReadValue(value) << MappingStart.Value;
		}
	}
}

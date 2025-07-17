using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Common.Fields
{
	public class BuildField
	{
		public string Name { get; private set; }
		public int Index { get; private set; }
		public int Width { get; private set; }

		public int? MappingStart { get; private set; }

		public int Count { get; private set; }

		private readonly Func<int> TotalCountGetter;

		public bool IsMapped
		{
			get { return MappingStart.HasValue; }
		}


		public bool SuffixCount
		{
			get { return  TotalCountGetter() > 1; }
		}

		public bool SuffixWidth { get; private set; }

		internal BuildField(string name, int index, int width, Func<int> totalCountGetter, bool suffixWidth = false)
		{
			Name = name;
			Index = index;
			Width = width;
			TotalCountGetter = totalCountGetter;
			SuffixWidth = suffixWidth;
			MappingStart = null;
			Count = 1;
		}

		internal BuildField(string name, int index, int width, Func<int> totalCountGetter, bool suffixWidth, int count)
			: this(name, index, width, totalCountGetter, suffixWidth)
		{
			Count = count;
		}

		internal BuildField(string name, int index, int width, int mappingStart, Func<int> totalCountGetter, bool suffixWidth = false)
			: this(name, index, width, totalCountGetter, suffixWidth)
		{
			MappingStart = mappingStart;
			Count = 1;
		}

		internal BuildField(string name, int index, int width, int mappingStart, int count, Func<int> totalCountGetter, bool suffixWidth = false)
			: this(name, index, width, mappingStart, totalCountGetter, suffixWidth)
		{
			Count = count;
		}

		public Field Build()
		{
			return new Field(Name, Index, Width, MappingStart, Count, SuffixCount, SuffixWidth);
		}
	}
}

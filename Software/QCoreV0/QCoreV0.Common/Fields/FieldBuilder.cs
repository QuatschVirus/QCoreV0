using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCoreV0.Common.Fields
{
	public class FieldBuilder
	{
		public int MaxWidth { get; private set; }

		private readonly List<BuildField> fields = [];

		private readonly Dictionary<string, int> counts = [];

		private int currentIndex = 0;

		public FieldBuilder(int maxWidth = 32)
		{
			MaxWidth = maxWidth;
		}

		public static FieldBuilder Create()
		{
			return new FieldBuilder(32);
		}

		public static FieldBuilder CreateHalfWord()
		{
			return new FieldBuilder(16);
		}

		public static FieldBuilder CreateDoubleWord()
		{
			return new FieldBuilder(64);
		}

		public static FieldBuilder CreateWord()
		{
			return new FieldBuilder(32);
		}

		public static FieldBuilder CreateByte()
		{
			return new FieldBuilder(8);
		}

		public FieldBuilder AddField(string name, int width, bool suffixWidth = false)
		{
			if (currentIndex + width > MaxWidth)
			{
				throw new ArgumentException($"Adding a new Field of width {width} exceeds maximum allowed width {MaxWidth}");
			}
			var count = counts.GetValueOrDefault(name, 0);
			fields.Add(new BuildField(name, currentIndex, width, () => counts.GetValueOrDefault(Field.GetWidthSuffixedName(name, width), 1), suffixWidth, count));
			counts[Field.GetWidthSuffixedName(name, width)] = count + 1;
			currentIndex += width;

			return this;
		}

		public FieldBuilder AddMappedField(string name, int width, int mappingStart, bool suffixWidth = false)
		{
			if (currentIndex + width > MaxWidth)
			{
				throw new ArgumentException($"Adding a new Mapped Field of width {width} exceeds maximum allowed width {MaxWidth}");
			}
			var count = counts.GetValueOrDefault(name, 0);
			fields.Add(new BuildField(name, currentIndex, width, mappingStart, count, () => counts.GetValueOrDefault(Field.GetWidthSuffixedName(name, width), 1), suffixWidth));
			counts[Field.GetWidthSuffixedName(name, width)] = count + 1;
			currentIndex += width;
			return this;
		}

		public FieldCollection Build()
		{
			var fieldList = fields.Select(f => f.Build()).ToList();
			return new FieldCollection(fieldList.ToDictionary(f => f.GetKey()));
		}
	}
}

namespace QCoreV0.Common.Fields
{
    public class FieldCollection
    {
        private readonly Dictionary<string, Field> fields;

		internal FieldCollection(Dictionary<string, Field> fields)
		{
			this.fields = fields;
		}

		public Field this[string key]
		{
			get
			{
				if (fields.TryGetValue(key, out var field))
				{
					return field;
				}
				throw new KeyNotFoundException($"Field '{key}' not found in the collection.");
			}
		}
		public bool HasField(string key)
		{
			return fields.ContainsKey(key);
		}
		public IEnumerable<Field> GetAllFields()
		{
			return fields.Values;
		}

		public int Count => fields.Count;

		public Dictionary<string, int> ComputeReadValues(int value)
		{
			return fields.ToDictionary(
				field => field.Key,
				field => field.Value.ComputeReadValue(value)
			);
		}

		public int ComputeWriteValue(string @base, int value)
		{
			return fields
				.Where(f => f.Value.GetBase() == @base)
				.Aggregate(0, (current, field) => current | field.Value.ComputeWriteValue(value));
		}
	}
}

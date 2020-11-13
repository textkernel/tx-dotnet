using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sovren.Utilities
{
	//https://github.com/dotnet/runtime/issues/30947
	internal class StringNullableEnumConverter<T> : JsonConverter<T>
	{
		private readonly Type _underlyingType;

		public StringNullableEnumConverter()
		{
			_underlyingType = Nullable.GetUnderlyingType(typeof(T));
		}

		public override bool CanConvert(Type typeToConvert)
		{
			return typeof(T).IsAssignableFrom(typeToConvert);
		}

		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			string value = reader.GetString();
			if (string.IsNullOrEmpty(value)) return default;

			try
			{
				return (T)Enum.Parse(_underlyingType, value);
			}
			catch (ArgumentException e)
			{
				throw new JsonException("Invalid value.", e);
			}
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value?.ToString());
		}
	}
}

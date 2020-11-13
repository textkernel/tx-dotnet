using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sovren
{
    //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to
    // we need a custom converter here since a T? property is, by default, not output as a json object with a .Value property
    // we do want it input/output that way though
    internal class SovrenNullableConverter<T> : JsonConverter<T?> where T : struct
    {
        private static readonly Type _underlyingType;

        static SovrenNullableConverter()
        {
            _underlyingType = typeof(T);
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(T?).IsAssignableFrom(typeToConvert) && _underlyingType != null;
        }

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();//malformed json
            }

            SovrenNullable<T> innerVal = null;

            // For performance, use the existing converter
            JsonConverter<T> valueConverter = (JsonConverter<T>)options.GetConverter(_underlyingType);

            if (valueConverter == null)
            {
                throw new JsonException("No converter found for type: " + _underlyingType);
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return innerVal?.Value;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();//malformed json
                }

                string propertyName = reader.GetString();
                reader.Read();//advance to the value

                switch (propertyName)
                {
                    case nameof(innerVal.Value):
                        {
                            innerVal = new SovrenNullable<T>();
                            innerVal.Value = valueConverter.Read(ref reader, _underlyingType, options);
                        }
                        break;
                    default: break;//invalid property, do not throw exception, json can have extra props
                }
            }

            throw new JsonException();//malformed json
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            SovrenNullable<T> innerVal = null;

            if (value != null)
            {
                innerVal = new SovrenNullable<T> { Value = value.Value };
            }

            JsonSerializer.Serialize(writer, innerVal, options);
        }

        private class SovrenNullable<TInner>
        {
            public TInner Value { get; set; }
        }
    }
}
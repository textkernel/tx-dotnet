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






//using System;
//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace Sovren
//{
//    //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to
//    // we need a custom converter here since a T? property is, by default, not output as a json object with a .Value property
//    // we do want it input/output that way though
//    internal class SovrenNullableConverter : JsonConverterFactory
//    {
//        public override bool CanConvert(Type typeToConvert)
//        {
//            Type innerType = Nullable.GetUnderlyingType(typeToConvert);
//            return innerType != null && (innerType.IsPrimitive || innerType == typeof(DateTime));
//        }

//        public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
//        {
//            Type genericType = type.GetGenericArguments()[0];

//            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
//                typeof(SovrenNullableConverterInner<>).MakeGenericType(new Type[] { genericType }),
//                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public,
//                binder: null,
//                args: new object[] { options },
//                culture: null);

//            return converter;
//        }

//        private class SovrenNullableConverterInner<T> : JsonConverter<T?> where T : struct
//        {
//            private readonly JsonConverter<T> _valueConverter;
//            private readonly Type _nullableType;

//            public SovrenNullableConverterInner(JsonSerializerOptions options)
//            {
//                // Cache the type
//                _nullableType = typeof(T);

//                // For performance, use the existing converter if available
//                _valueConverter = (JsonConverter<T>)options.GetConverter(_nullableType);

//                if (_valueConverter == null)
//                {
//                    throw new JsonException("No converter found for type: " + _nullableType);
//                }
//            }

//            public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//            {
//                if (reader.TokenType != JsonTokenType.StartObject)
//                {
//                    throw new JsonException();//malformed json
//                }

//                T? nullable = null;

//                while (reader.Read())
//                {
//                    if (reader.TokenType == JsonTokenType.EndObject)
//                    {
//                        return nullable;
//                    }

//                    if (reader.TokenType != JsonTokenType.PropertyName)
//                    {
//                        throw new JsonException();//malformed json
//                    }

//                    string propertyName = reader.GetString();
//                    reader.Read();//advance to the value

//                    switch (propertyName)
//                    {
//                        case nameof(nullable.Value):
//                            {
//                                nullable = _valueConverter.Read(ref reader, _nullableType, options);
//                            }
//                            break;
//                        default: break;//invalid property, do not throw exception, json can have extra props
//                    }
//                }

//                throw new JsonException();//malformed json
//            }

//            public override void Write(Utf8JsonWriter writer, T? item, JsonSerializerOptions options)
//            {
//                if (item != null)
//                {
//                    writer.WriteStartObject();

//                    //write the Value property, if it the nullable has a value
//                    writer.WritePropertyName(nameof(item.Value));
//                    _valueConverter.Write(writer, item.Value, options);

//                    writer.WriteEndObject();
//                }
//                else
//                {
//                    writer.WriteNullValue();//this should never happen in our json output since we suppress nulls
//                }
//            }
//        }


//    }
//}


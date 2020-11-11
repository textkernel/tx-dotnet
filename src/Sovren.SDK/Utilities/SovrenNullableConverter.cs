// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sovren
{
    //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to
    internal class SovrenNullableConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
            {
                return false;
            }

            if (typeToConvert.GetGenericTypeDefinition() != typeof(SovrenNullable<>))
            {
                return false;
            }

            Type subType = typeToConvert.GetGenericArguments()[0];
            return subType.IsPrimitive || subType == typeof(DateTime);
        }

        public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
        {
            Type genericType = type.GetGenericArguments()[0];

            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(SovrenNullableJsonConverterInner<>).MakeGenericType(new Type[] { genericType }),
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null);

            return converter;
        }

        private class SovrenNullableJsonConverterInner<T> : JsonConverter<SovrenNullable<T>>
        {
            private readonly JsonConverter<T> _valueConverter;
            private readonly Type _nullableType;

            public SovrenNullableJsonConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available
                _valueConverter = (JsonConverter<T>)options.GetConverter(typeof(T));

                // Cache the type
                _nullableType = typeof(T);
            }

            public override SovrenNullable<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();//malformed json
                }

                SovrenNullable<T> nullable = new SovrenNullable<T>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return nullable;
                    }

                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {
                        throw new JsonException();//malformed json
                    }

                    string propertyName = reader.GetString();
                    reader.Read();//advance to the value

                    switch (propertyName)
                    {
                        case nameof(nullable.HasValue):
                            nullable.HasValue = reader.GetBoolean();
                            break;
                        case nameof(nullable.Value):
                            {
                                if (_valueConverter != null)
                                {
                                    nullable.Value = _valueConverter.Read(ref reader, _nullableType, options);
                                }
                                else
                                {
                                    nullable.Value = JsonSerializer.Deserialize<T>(ref reader, options);
                                }
                            }
                            break;
                        default: break;//invalid property in SovrenNullable, do not throw exception, json can have extra props
                    }
                }

                throw new JsonException();//malformed json
            }

            public override void Write(Utf8JsonWriter writer, SovrenNullable<T> nullable, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                //write the HasValue property
                writer.WriteBoolean(nameof(nullable.HasValue), nullable.HasValue);

                if (nullable.HasValue)
                {
                    //write the Value property, if it the nullable has a value
                    writer.WritePropertyName(nameof(nullable.Value));
                    if (_valueConverter != null)
                    {
                        _valueConverter.Write(writer, nullable.Value, options);
                    }
                    else
                    {
                        JsonSerializer.Serialize(writer, nullable.Value, options);
                    }
                }

                writer.WriteEndObject();
            }
        }
    }
}

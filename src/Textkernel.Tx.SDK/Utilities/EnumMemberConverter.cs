// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Textkernel.Tx
{
    internal class EnumMemberConverter<T> : JsonConverter<T> where T : Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(GetDescription(value));
        }

        private static string GetDescription(Enum e)
        {
            if (e == null) return null;

            Type type = e.GetType();
            string name = Enum.GetName(type, e);
            if (name == null) return e.ToString();

            FieldInfo field = type.GetField(name);
            if (field == null) return e.ToString();

            if (Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) is EnumMemberAttribute attr)
            {
                return attr.Value;
            }
            return e.ToString();
        }
    }
}

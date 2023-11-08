// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sovren
{
    //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to
    internal class IntConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //this is a fix for https://github.com/textkernel/tx-dotnet/issues/54
            return (int)reader.GetDouble();
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}

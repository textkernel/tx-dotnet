// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Text.Encodings.Web;
using System.Text.Json;

namespace Textkernel.Tx
{
    internal static class SovrenJsonSerialization
    {
        internal static JsonSerializerOptions DefaultOptions
        {
            get
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    IgnoreNullValues = true
                };
                options.Converters.Add(new DateTimeConverter());
                return options;
            }
        }
    }
}

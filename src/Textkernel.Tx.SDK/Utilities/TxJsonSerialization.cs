// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Text.Encodings.Web;
using System.Text.Json;

namespace Textkernel.Tx
{
    internal static class TxJsonSerialization
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

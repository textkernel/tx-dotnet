// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Text.Encodings.Web;
using System.Text.Json;

namespace Textkernel.Tx
{
    /// <summary>
    /// Default options that work well to serialize Tx objects to/from JSON using System.Text.Json
    /// </summary>
    public static class TxJsonSerialization
    {
        /// <summary>
        /// Create a default set of options and converters for Tx models
        /// </summary>
        public static JsonSerializerOptions CreateDefaultOptions()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
            options.Converters.Add(new DateTimeConverter());
            options.Converters.Add(new IntConverter());
            return options;
        }
    }
}

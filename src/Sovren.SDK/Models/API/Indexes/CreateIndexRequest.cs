// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Matching;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.Indexes
{
    /// <summary>
    /// Request body to create an index
    /// </summary>
    public class CreateIndexRequest
    {
        /// <summary>
        /// The type of documents this index will contain
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public IndexType IndexType { get; set; }
    }
}

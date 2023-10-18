// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Matching;
using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.API.Indexes
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

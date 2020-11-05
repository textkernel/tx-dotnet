// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Text.Json.Serialization;

namespace Sovren.Models.Matching
{
    /// <summary>
    /// A document index to hold resumes or jobs
    /// </summary>
    public class Index
    {
        /// <summary>
        /// The account id of the owner for this index
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// The name/id of this index
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of document in this index
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public IndexType IndexType { get; set; }
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Matching.Response
{
    /// <summary>
    /// A single result from a search query
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// The document id of the search/match result
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The id of the index containing the document
        /// </summary>
        public string IndexId { get; set; }
    }
}

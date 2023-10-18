// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.Matching.Response
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

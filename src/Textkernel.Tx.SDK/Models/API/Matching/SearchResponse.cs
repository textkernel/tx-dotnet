// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.Matching.Response;
using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.Matching
{
    /// <inheritdoc/>
    public class SearchResponse : ApiResponse<SearchResponseValue> { }

    /// <summary>
    /// Base class for searches/matches response values
    /// </summary>
    public class BaseSearchMatchResponseValue<T>
    {
        /// <summary>
        /// The list of matches for the search/match
        /// </summary>
        public List<T> Matches { get; set; }

        /// <summary>
        /// The number of results returned in this response
        /// </summary>
        public int CurrentCount { get; set; }

        /// <summary>
        /// The total number of results that fit the query/criteria
        /// </summary>
        public int TotalCount { get; set; }
    }

    /// <summary>
    /// The <see cref="ApiResponse{T}.Value"/> from a Search response
    /// </summary>
    public class SearchResponseValue : BaseSearchMatchResponseValue<SearchResult> { }
}

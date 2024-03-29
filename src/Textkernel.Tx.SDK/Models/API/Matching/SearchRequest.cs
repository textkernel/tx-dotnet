// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.Matching.Request;

namespace Textkernel.Tx.Models.API.Matching
{
    /// <summary>
    /// Request body for a search request
    /// </summary>
    public class SearchRequest : SearchMatchRequestBase
    {
        /// <summary>
        /// Used to choose which results to return from the list.
        /// </summary>
        public PaginationSettings PaginationSettings { get; set; }
    }
}

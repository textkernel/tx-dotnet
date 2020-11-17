// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Matching.Request
{
    /// <summary>
    /// Settings for pagination of results
    /// </summary>
    public class PaginationSettings
    {
        /// <summary>
        /// How many results to return
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// How many results to skip. For example: (skip 5, take 10) means return results 6-15
        /// </summary>
        public int Skip { get; set; }
    }
}

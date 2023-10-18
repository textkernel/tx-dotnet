// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.Matching.Request
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

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.Matching.Response
{
    /// <inheritdoc/>
    public class SimpleCategoryScoreData : CategoryScoreData
    {
        /// <summary>
        /// List of terms found in both source and target documents
        /// </summary>
        public List<string> Found { get; set; }

        /// <summary>
        /// List of terms requested but not found
        /// </summary>
        public List<string> NotFound { get; set; }
    }
}

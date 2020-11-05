// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.API.Matching.Response
{
    /// <summary>
    /// Details about the score for a specific category
    /// </summary>
    public abstract class CategoryScoreData
    {
        /// <summary>
        /// An unweighted score from 0-100. This is the percentage match of this category.
        /// </summary>
        public double UnweightedScore { get; set; }

        /// <summary>
        /// Detailed written explanation about each data point found or not found.
        /// </summary>
        public List<CategoryScoreEvidence> Evidence { get; set; }
    }
}

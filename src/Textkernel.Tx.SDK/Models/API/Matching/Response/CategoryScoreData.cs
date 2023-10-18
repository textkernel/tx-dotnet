// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.Matching.Response
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

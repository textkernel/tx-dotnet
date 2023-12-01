// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.Matching.Response;

namespace Textkernel.Tx.Models.API.BimetricScoring
{
    /// <summary>
    /// And individual result (representing a single document) for a 'BimetricScore' request
    /// </summary>
    public class BimetricScoreResult : IBimetricScoredResult
    {
        /// <summary>
        /// The document id of the result
        /// </summary>
        public string Id { get; set; }

        /// <inheritdoc/>
        public int SovScore { get; set; }

        /// <inheritdoc/>
        public int WeightedScore { get; set; }
        
        /// <inheritdoc/>
        public int ReverseCompatibilityScore { get; set; }

        /// <inheritdoc/>
        public EnrichedScoreData EnrichedScoreData { get; set; }

        /// <inheritdoc/>
        public EnrichedScoreData EnrichedRCSScoreData { get; set; }
    }
}

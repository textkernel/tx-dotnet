// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Matching.Response;

namespace Sovren.Models.API.BimetricScoring
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

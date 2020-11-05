// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Matching.Response
{
    /// <summary>
    /// A single result containing information about the bimetric score between a target and source document
    /// </summary>
    public interface IBimetricScoredResult
    {
        /// <summary>
        /// An integer score representing the overall fit of the match.
        /// This is the result of a proprietary algorithm that combines the
        /// <see cref="WeightedScore"/> and the <see cref="ReverseCompatibilityScore"/> 
        /// into one overall score. Results are sorted by this parameter in descending order.
        /// </summary>
        int SovScore { get; set; }

        /// <summary>
        /// An integer score from 0-100 representing how well the current document matched the source document. 
        /// This calculation is the sum of the unweighted category scores multiplied by their respective applied weight.
        /// A score of 100 means that all of the data points in the source document were found in the target document, 
        /// but the target document may have had many extra data points.
        /// <br/>
        /// <br/>
        /// See also:
        /// <br/> <seealso cref="CategoryScoreData.UnweightedScore"/>
        /// <br/> <seealso cref="BaseScoredResponseValue{T}.AppliedCategoryWeights"/>
        /// </summary>
        int WeightedScore { get; set; }

        /// <summary>
        /// An integer score from 0-100 which represents how well the target document matched to the source document.
        /// This is equivalent to the <see cref="WeightedScore"/> if you ran the match/score with the source and 
        /// target documents swapped. A score of 100 means that all of the data points in the target document were found
        /// in the source document, but the source document may have had many extra data points.
        /// </summary>
        int ReverseCompatibilityScore { get; set; }

        /// <summary>
        /// Detailed information/evidence about the <see cref="WeightedScore"/>
        /// </summary>
        EnrichedScoreData EnrichedScoreData { get; set; }

        /// <summary>
        /// Detailed information/evidence about the <see cref="ReverseCompatibilityScore"/>
        /// </summary>
        EnrichedScoreData EnrichedRCSScoreData { get; set; }
    }

    /// <inheritdoc cref="IBimetricScoredResult"/>
    public class MatchResult : SearchResult, IBimetricScoredResult
    {
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

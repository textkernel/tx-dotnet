// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.Matching.Response;

namespace Textkernel.Tx.Models.API.Matching
{
    /// <summary>
    /// A base class for all scored responses
    /// </summary>
    /// <typeparam name="T">The type of match result</typeparam>
    public class BaseScoredResponseValue<T> : BaseSearchMatchResponseValue<T>
    {
        /// <summary>
        /// The weights suggested based solely on the data in the source document.
        /// <br/>NOTE: these should only be used as a fallback or initial value. Your system/users
        /// should have the ability to adjust/override these (in the PreferredCategoryWeights in the request)
        /// <br/>
        /// <br/>
        /// See also:
        /// <br/><seealso cref="BimetricScoring.BimetricScoreRequest.PreferredCategoryWeights"/>
        /// <br/><seealso cref="Request.MatchRequest.PreferredCategoryWeights"/>
        /// </summary>
        public CategoryWeights SuggestedCategoryWeights { get; set; }

        /// <summary>
        /// The weights that were actually used for scoring. These are either
        /// <br/>1) if the PreferredCategoryWeights are specified in the request, these are used (with any adjustments for non-applicable categories)
        /// <br/>2) otherwise these are simply the <see cref="SuggestedCategoryWeights"/>
        /// <br/>
        /// <br/>
        /// See also:
        /// <br/><seealso cref="BimetricScoring.BimetricScoreRequest.PreferredCategoryWeights"/>
        /// <br/><seealso cref="Request.MatchRequest.PreferredCategoryWeights"/>
        /// </summary>
        public CategoryWeights AppliedCategoryWeights { get; set; }
    }

    /// <summary>
    /// The <see cref="ApiResponse{T}.Value"/> from a Match response
    /// </summary>
    public class MatchResponseValue : BaseScoredResponseValue<MatchResult> { }
}

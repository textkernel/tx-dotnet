// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Matching.Response;

namespace Sovren.Models.API.Matching
{
    /// <summary>
    /// A base class for all scored responses
    /// </summary>
    /// <typeparam name="T">The type of match result</typeparam>
    public class BaseScoredResponseValue<T> : BaseSearchMatchResponseValue<T>
    {
        /// <summary>
        /// The weights suggested by Sovren based solely on the data in the source document.
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

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.Matching.Request
{
    /// <summary>
    /// Request body for a Match request
    /// </summary>
    public abstract class MatchRequest : SearchMatchRequestBase
    {
        /// <summary>
        /// The number of results to return.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// The weights you want to use for scoring. <b>It is important to specify these, otherwise default
        /// values will be used. </b>
        /// <br/>
        /// <br/>
        /// These weights will be used except in the case
        /// that you provided a non-zero weight for a category that is irrelevant in the source document.
        /// For example, this can happen when the source document contains no languages.
        /// <br/>See also: <seealso cref="BaseScoredResponseValue{T}.AppliedCategoryWeights"/>
        /// </summary>
        public CategoryWeights PreferredCategoryWeights { get; set; }
    }
}

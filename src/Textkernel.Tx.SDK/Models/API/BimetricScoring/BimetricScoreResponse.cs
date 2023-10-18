// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.Matching;

namespace Textkernel.Tx.Models.API.BimetricScoring
{
    /// <inheritdoc/>
    public class BimetricScoreResponse : ApiResponse<BimetricScoreResponseValue> { }

    /// <summary>
    /// The <see cref="ApiResponse{T}.Value"/> from a 'BimetricScore' response
    /// </summary>
    public class BimetricScoreResponseValue : BaseScoredResponseValue<BimetricScoreResult> { }
}

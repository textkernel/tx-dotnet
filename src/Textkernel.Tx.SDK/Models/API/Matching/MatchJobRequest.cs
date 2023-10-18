// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.Matching.Request;
using Textkernel.Tx.Models.Job;

namespace Textkernel.Tx.Models.API.Matching
{
    /// <inheritdoc/>
    public class MatchJobRequest : MatchRequest
    {
        /// <summary>
        /// The job to match. This should be generated by parsing a job with the Sovren Job Parser.
        /// </summary>
        public ParsedJob JobData { get; set; }
    }
}

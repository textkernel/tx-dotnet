// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.Matching.Response
{
    /// <summary>
    /// Contains information about why the score is a certain value
    /// </summary>
    public class CategoryScoreEvidence
    {
        /// <summary>
        /// Information regarding the outcome of one or more of the data points in the query.
        /// </summary>
        public string Fact { get; set; }

        /// <summary>
        /// The sentiment of the <see cref="Fact"/>. This also indicates if this evidence led to a higher or lower score. One of:
        /// <br/> Negative
        /// <br/> Positive
        /// <br/> Mixed
        /// </summary>
        public string Type { get; set; }
    }
}

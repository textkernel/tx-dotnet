// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Matching.Response
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

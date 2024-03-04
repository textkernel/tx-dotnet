// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;

namespace Textkernel.Tx.Models.Resume.Education
{
    /// <summary>
    /// A normalized educational degree
    /// </summary>
    public class NormalizedDegree
    {
        /// <summary>
        /// One of the codes listed <see href="https://developer.textkernel.com/tx-platform/v10/resume-parser/overview/normalized-education-codes/">here</see>.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// One of the descriptions listed <see href="https://developer.textkernel.com/tx-platform/v10/resume-parser/overview/normalized-education-codes/">here</see>.
        /// </summary>
        public string Description { get; set; }
    }
}

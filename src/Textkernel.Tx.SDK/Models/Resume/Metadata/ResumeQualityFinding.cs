// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.Resume.Metadata
{
    /// <summary>
    /// A single resume quality issue
    /// </summary>
    public class ResumeQualityFinding
    {
        /// <summary>
        /// A unique 3-digit code to identify what type of issue was found.
        /// See all possibilities at our docs site <see href="https://developer.textkernel.com/tx-platform/v10/resume-parser/overview/parser-output/">here</see>.
        /// </summary>
        public string QualityCode { get; set; }

        /// <summary>
        /// If applicable, areas in the resume where this issue was found or that are affected by this issue.
        /// </summary>
        public List<SectionIdentifier> SectionIdentifiers { get; set; }

        /// <summary>
        /// A human-readable message explaining the issue that is being reported and possibly how to fix.
        /// </summary>
        public string Message { get; set; }
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Parsing;
using System.Collections.Generic;

namespace Sovren.Models.Resume.Metadata
{
    /// <summary>
    /// Metadata about a parsed resume
    /// </summary>
    public class ResumeMetadata : ParsedDocumentMetadata
    {
        /// <summary>
        /// A list of sections found in the resume
        /// </summary>
        public List<ResumeSection> FoundSections { get; set; }

        /// <summary>
        /// A list of quality assessments for the resume. These are very useful for
        /// providing feedback to candidates about why their resume did not parse properly.
        /// These can also be used to determine if a resume is 'high quality' enough to put into
        /// your system.
        /// </summary>
        public List<ResumeQualityAssessment> ResumeQuality { get; set; }

        /// <summary>
        /// Used by Sovren to redact PII. See <see cref="ParseResumeResponseValue.RedactedResumeData"/>
        /// </summary>
        public ReservedData ReservedData { get; set; }
    }
}

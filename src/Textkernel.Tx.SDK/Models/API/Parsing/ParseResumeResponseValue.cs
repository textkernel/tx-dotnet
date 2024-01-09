// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Resume;

namespace Textkernel.Tx.Models.API.Parsing
{
    /// <inheritdoc/>
    public class ParseResumeResponseValue : BaseParseResponseValue
    {
        /// <summary>
        /// The main output from the Resume Parser
        /// </summary>
        public ParsedResume ResumeData { get; set; }

        /// <summary>
        /// Similar to <see cref="ResumeData"/>, but with all of the 
        /// Personally Identifiable Information (PII) redacted. For example,
        /// this property will contain no <see cref="ParsedResume.ContactInformation"/>.
        /// </summary>
        public ParsedResume RedactedResumeData { get; set; }

        /// <summary>
        /// Information about the FlexRequests transaction, if any were provided.
        /// </summary>
        public FlexResponse FlexResponse { get; set; }
    }
}

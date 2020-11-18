// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Resume;

namespace Sovren.Models.API.Parsing
{
    /// <inheritdoc/>
    public class ParseResumeResponseValue : BaseParseResponseValue
    {
        /// <summary>
        /// The main output from the Sovren Resume Parser
        /// </summary>
        public ParsedResume ResumeData { get; set; }

        /// <summary>
        /// Similar to <see cref="ResumeData"/>, but with all of the 
        /// Personally Identifiable Information (PII) redacted. For example,
        /// this property will contain no <see cref="ParsedResume.ContactInformation"/>.
        /// </summary>
        public ParsedResume RedactedResumeData { get; set; }
    }
}

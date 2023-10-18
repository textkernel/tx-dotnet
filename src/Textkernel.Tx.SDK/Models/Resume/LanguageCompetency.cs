// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume
{
    /// <summary>
    /// A language competency (fluent in, can read, can write, etc) found on a resume
    /// </summary>
    public class LanguageCompetency
    {
        /// <summary>
        /// The language name
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The two-letter ISO 639-1 code for the language
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// The full text where Sovren found this language competency
        /// </summary>
        public string FoundInContext { get; set; }
    }
}

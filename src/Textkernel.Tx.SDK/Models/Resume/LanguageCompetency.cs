// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Resume
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

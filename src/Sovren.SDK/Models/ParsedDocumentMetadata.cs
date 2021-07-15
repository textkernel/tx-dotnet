// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;

namespace Sovren.Models
{
    /// <summary>
    /// Metadata about a parsed document
    /// </summary>
    public class ParsedDocumentMetadata
    {
        /// <summary>
        /// The plain text that was used for parsing
        /// </summary>
        public string PlainText { get; set; }

        /// <summary>
        /// An ISO 639-1 code that represents the primary language of the parsed text. When the
        /// language could not be automatically determined, it is reported as the special value
        /// <c> iv </c>(invariant/unknown). Note that the two-letter ISO codes reported by the
        /// Parser - such as <c> zh </c> for Chinese - do not differentiate between language
        /// variants, such as Mandarin and Cantonese.
        /// </summary>
        public string DocumentLanguage { get; set; }

        /// <summary>
        /// An ISO 3066 code that represents the cultural context of the document regarding formatting of
        /// numbers, dates, character symbols, etc. This value is usually a simple concatenation of the 
        /// language and country codes, such as <c>en-US</c> for US English; however, note that culture
        /// can be set independently of language and country to achieve fine-tuned cultural control over parsing,
        /// so if you use this value you should not assume that it always matches the language and country.
        /// </summary>
        public string DocumentCulture { get; set; }

        /// <summary>
        /// The full parser settings that were used during parsing
        /// </summary>
        public string ParserSettings { get; set; }

        /// <summary>
        /// The last-revised/last-modified date that was provided for the document.
        /// This was used to calculate all of the important metrics about skills and jobs.
        /// </summary>
        public DateTime DocumentLastModified { get; set; }

        /// <summary>
        /// A digital signature used to ensure there is no tampering between
        /// parsing and indexing. This prevents Sovren from storing any PII in
        /// the AI Matching engine.
        /// </summary>
        public List<string> SovrenSignature { get; set; }
    }
}

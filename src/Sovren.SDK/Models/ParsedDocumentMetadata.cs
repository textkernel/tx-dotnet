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
        /// The two-letter ISO 639-1 code for the language the document was written in
        /// </summary>
        public string DocumentLanguage { get; set; }

        /// <summary>
        /// The xx-XX language/culture value for the parsed document. See also <see cref="DocumentLanguage"/>
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

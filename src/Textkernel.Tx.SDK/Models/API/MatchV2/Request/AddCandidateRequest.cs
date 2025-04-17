// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;
using Textkernel.Tx.Models.Resume;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    /// <summary>
    /// Request body for AddCandidate request
    /// </summary>
    public class AddCandidateRequest : AddDocumentRequestBase
    {
        /// <summary>
        /// A boolean flag to strip PII data out of the requests before sending to Search &amp; Match V2
        /// </summary>
        public bool Anonymize { get; set; }

        /// <summary>
        /// Parsed output from the CV/Resume Parser. 
        /// </summary>
        public ParsedResume ResumeData { get; set; }

        /// <summary>
        /// A collection of custom fields represented as key-value pairs.
        /// </summary>
        public Dictionary<string, string> CustomFields { get; set; }
    }
}

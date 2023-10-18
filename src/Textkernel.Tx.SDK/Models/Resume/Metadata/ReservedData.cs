// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.Resume.Metadata
{
    /// <summary>
    /// Used to redact PII
    /// </summary>
    public class ReservedData
    {
        /// <summary>
        /// All phone numbers found in the resume
        /// </summary>
        public List<string> Phones { get; set; }

        /// <summary>
        /// All names found in the resume
        /// </summary>
        public List<string> Names { get; set; }

        /// <summary>
        /// All email addresses found in the resume
        /// </summary>
        public List<string> EmailAddresses { get; set; }

        /// <summary>
        /// All personal urls found in the resume
        /// </summary>
        public List<string> Urls { get; set; }

        /// <summary>
        /// Any other PII that should be redacted
        /// </summary>
        public List<string> OtherData { get; set; }
    }
}

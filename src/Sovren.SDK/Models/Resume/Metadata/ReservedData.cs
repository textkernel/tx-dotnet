// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Resume.Metadata
{
    /// <summary>
    /// Used by Sovren to redact PII
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

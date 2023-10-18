// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Resume.ContactInfo;
using System.Collections.Generic;

namespace Sovren.Models.Resume
{
    /// <summary>
    /// A reference found on a resume
    /// </summary>
    public class CandidateReference
    {
        /// <summary>
        /// The name of the reference
        /// </summary>
        public PersonName ReferenceName { get; set; }

        /// <summary>
        /// The job title of the reference
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The employer of the reference
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The type of reference
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The physical location of the reference
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Phone numbers listed for the reference
        /// </summary>
        public List<NormalizedString> Telephones { get; set; }

        /// <summary>
        /// Email addresses listed for the reference
        /// </summary>
        public List<string> EmailAddresses { get; set; }

        /// <summary>
        /// Other web addresses listed for the reference
        /// </summary>
        public List<WebAddress> WebAddresses { get; set; }
    }
}

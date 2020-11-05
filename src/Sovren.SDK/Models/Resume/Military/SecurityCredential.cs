// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume.Military
{
    /// <summary>
    /// A security credential/clearance found on a resume
    /// </summary>
    public class SecurityCredential
    {
        /// <summary>
        /// The name of the credential/clearance
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The full context of where Sovren found this clearance/credential
        /// </summary>
        public string FoundInContext { get; set; }
    }
}

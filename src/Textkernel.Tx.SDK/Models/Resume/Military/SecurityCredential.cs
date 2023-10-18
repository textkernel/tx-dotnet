// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Resume.Military
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
        /// The full context of where this clearance/credential was found
        /// </summary>
        public string FoundInContext { get; set; }
    }
}

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Resume
{
    /// <summary>
    /// An association/organization found on a resume
    /// </summary>
    public class Association
    {
        /// <summary>
        /// The name of the association/organization
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// The role the candidate held
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// The full text in which this association was found
        /// </summary>
        public string FoundInContext { get; set; }
    }
}

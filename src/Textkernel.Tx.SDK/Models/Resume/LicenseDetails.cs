// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Resume
{
    /// <summary>
    /// A license found on a resume. These are professional licenses, not driving licenses.
    /// For driving licenses, see <see cref="PersonalAttributes.DrivingLicense"/>
    /// </summary>
    public class LicenseDetails
    {
        /// <summary>
        /// The name of the license
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// <see langword="true"/> if this was found by matching to a known list of licenses.
        /// <see langword="false"/> if this was found by analyzing the context and determining it was a license.
        /// </summary>
        public bool MatchedFromList { get; set; }
    }
}

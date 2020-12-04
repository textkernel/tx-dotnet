// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume
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
        /// <see langword="true"/> if Sovren found this by matching to a known list of licenses.
        /// <see langword="false"/> if Sovren found this by analyzing the context and determining it was a license.
        /// </summary>
        public bool MatchedFromList { get; set; }
    }
}

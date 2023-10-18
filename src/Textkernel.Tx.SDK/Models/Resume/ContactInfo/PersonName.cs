// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume.ContactInfo
{
    /// <summary>
    /// A name broken into its constituent parts
    /// </summary>
    public class PersonName
    {
        /// <summary>
        /// The full name in a standard format
        /// </summary>
        public string FormattedName { get; set; }

        /// <summary>
        /// A prefix for a name, such as Dr.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// The given (first) name
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// The middle name or initial
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// The nickname/moniker, this is rarely populated
        /// </summary>
        public string Moniker { get; set; }

        /// <summary>
        /// The family (last) name
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// A suffix for a name, such as Jr. or III
        /// </summary>
        public string Suffix { get; set; }
    }
}

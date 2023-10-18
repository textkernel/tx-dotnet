// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Resume.ContactInfo
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

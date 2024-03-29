// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.Geocoding
{
    /// <summary>
    /// An address used to geocode a document
    /// </summary>
    public class Address
    {
        /// <summary>
        /// The 2-letter ISO 3166 country code
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The Postal or Zip code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// The Region/District/State
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// The City/Municipality/Town
        /// </summary>
        public string Municipality { get; set; }

        /// <summary>
        /// Street address
        /// </summary>
        public string AddressLine { get; set; }
    }
}

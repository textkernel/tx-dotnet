// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models
{
    /// <summary>
    /// Represents a physical location on Earth (mostly used for addresses)
    /// </summary>
    public class Location
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
        /// The Regions/Districts/States
        /// </summary>
        public List<string> Regions { get; set; }

        /// <summary>
        /// The City/Municipality/Town
        /// </summary>
        public string Municipality { get; set; }

        /// <summary>
        /// Street address lines
        /// </summary>
        public List<string> StreetAddressLines { get; set; }

        /// <summary>
        /// If geocoding has been done, this is the lat/lon for the location
        /// </summary>
        public GeocodedCoordinates GeoCoordinates { get; set; }
    }

    /// <summary>
    /// Represents a lat/lon provided by a 3rd party service
    /// </summary>
    public class GeocodedCoordinates : GeoCoordinates
    {
        /// <summary>
        /// The geocoding source, such as Google or Bing
        /// </summary>
        public string Source { get; set; }
    }

    /// <summary>
    /// Represents a lat/lon
    /// </summary>
    public class GeoCoordinates
    {
        /// <summary>
        /// The latitude, in degrees
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude, in degrees
        /// </summary>
        public double Longitude { get; set; }
    }
}

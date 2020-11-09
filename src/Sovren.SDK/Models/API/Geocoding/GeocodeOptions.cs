// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Text.Json.Serialization;

namespace Sovren.Models.API.Geocoding
{
    /// <inheritdoc/>
    public class GeocodeOptions : GeocodeOptionsBase
    {
        /// <summary>
        /// <see langword="true"/> to geocode, otherwise <see langword="false"/>
        /// </summary>
        public bool IncludeGeocoding { get; set; }
    }

    /// <summary>
    /// Options for geocoding a document (specifying some location on Earth)
    /// </summary>
    public class GeocodeOptionsBase : GeocodeCredentials
    {
        /// <summary>
        /// The address you wish to geocode. This field is optional. <b>If you specify this value,
        /// this address will be used to get the geocode coordinates instead of the address included
        /// in the parsed document (if present); however, the address in the parsed document will not be modified.</b>
        /// </summary>
        public Address PostalAddress { get; set; }

        /// <summary>
        /// The geographic coordinates (latitude/longitude) for your postal address. <b>Use this if you already 
        /// have latitude/longitude coordinates and simply wish to add them to your parsed document. If provided, 
        /// these values will be inserted into your parsed document and the address included in the 
        /// parsed document (if present), will not be modified.</b>
        /// </summary>
        public GeoCoordinates GeoCoordinates { get; set; }
    }

    /// <summary>
    /// Credentials for geocoding
    /// </summary>
    public class GeocodeCredentials
    {
        /// <summary>
        /// The provider you wish to use to geocode the postal address. If you use <see cref="GeocodeProvider.Bing"/>
        /// you must specify your <see cref="ProviderKey"/>
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GeocodeProvider Provider { get; set; }

        /// <summary>
        /// Your private API key for the geocoding provider. If using <see cref="GeocodeProvider.Bing"/> you must specify your own API key.
        /// <br/>If using <see cref="GeocodeProvider.Google"/>, you can optionally provide your own API key
        /// </summary>
        public string ProviderKey { get; set; }
    }
}

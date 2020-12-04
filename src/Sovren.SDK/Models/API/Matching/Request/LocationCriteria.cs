// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Geocoding;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.Matching.Request
{
    /// <summary>
    /// Units for distance
    /// </summary>
    public enum DistanceUnit
    {
        /// <summary>
        /// Miles
        /// </summary>
        Miles,

        /// <summary>
        /// Kilometers
        /// </summary>
        Kilometers
    }

    /// <summary>
    /// Criteria for distance/range filtering
    /// </summary>
    public class LocationCriteria
    {
        /// <summary>
        /// Results must be found within a certain distance of one of these specified locations.
        /// </summary>
        public List<Location> Locations { get; set; }

        /// <summary>
        /// The distance from <see cref="Locations"/> within which to find results.
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// The units for the specified distance. 
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DistanceUnit DistanceUnit { get; set; }

        /// <summary>
        /// The provider to lookup latitude/longitude if they are not specified. If you use <see cref="GeocodeProvider.Bing"/>
        /// you must specify your <see cref="GeocodeProviderKey"/>
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GeocodeProvider GeocodeProvider { get; set; }

        /// <summary>
        /// Your private API key for the geocoding provider. If using <see cref="GeocodeProvider.Bing"/> you must specify your own API key.
        /// <br/>If using <see cref="GeocodeProvider.Google"/>, you can optionally provide your own API key
        /// </summary>
        public string GeocodeProviderKey { get; set; }
    }
}

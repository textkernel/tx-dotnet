// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Geocoding
{
    /// <summary>
    /// Geocode service providers
    /// </summary>
    public enum GeocodeProvider
    {
        /// <summary>
        /// Google Maps geocoding service
        /// </summary>
        Google,

        /// <summary>
        /// Bing geocoding service
        /// </summary>
        Bing,

        /// <summary>
        /// No geocoding
        /// </summary>
        None
    }
}

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.Geocoding
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

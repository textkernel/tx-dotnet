﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Resume;

namespace Textkernel.Tx.Models.API.Geocoding
{
    /// <inheritdoc/>
    public class GeocodeResumeResponse : ApiResponse<GeocodeResumeResponseValue> { }

    /// <summary>
    /// The <see cref="ApiResponse{T}.Value"/> from a geocode resume response
    /// </summary>
    public class GeocodeResumeResponseValue
    {
        /// <summary>
        /// The resume you sent to be geocoded with geolocation coordinates added
        /// </summary>
        public ParsedResume ResumeData { get; set; }
    }
}

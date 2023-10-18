﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Textkernel.Tx.Models.Job;

namespace Textkernel.Tx.Models.API.Geocoding
{
    /// <inheritdoc/>
    public class GeocodeJobResponse : ApiResponse<GeocodeJobResponseValue> { }

    /// <summary>
    /// The <see cref="ApiResponse{T}.Value"/> from a geocode job response
    /// </summary>
    public class GeocodeJobResponseValue
    {
        /// <summary>
        /// The job you sent to be geocoded with geolocation coordinates added
        /// </summary>
        public ParsedJob JobData { get; set; }
    }
}

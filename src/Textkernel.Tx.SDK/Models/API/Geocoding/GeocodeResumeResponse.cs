// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Resume;

namespace Sovren.Models.API.Geocoding
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

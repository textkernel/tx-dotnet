// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Job;
using Sovren.Models.Resume;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.Geocoding
{
    /// <inheritdoc/>
    public class GeocodeAndIndexResumeResponse : ApiResponse<GeocodeAndIndexResumeResponseValue> { }

    /// <inheritdoc/>
    public class GeocodeAndIndexJobResponse : ApiResponse<GeocodeAndIndexJobResponseValue> { }

    /// <summary>
    /// The <see cref="ApiResponse{T}.Value"/> from a geocode and index response
    /// </summary>
    public class GeocodeAndIndexResponseValue
    {
        /// <summary>
        /// If geocoding was requested, the status of the geocode transaction will be output here
        /// </summary>
        public ApiResponseInfoLite GeocodeResponse { get; set; }

        /// <summary>
        /// If indexing was requested, the status of the index transaction will be output here
        /// </summary>
        public ApiResponseInfoLite IndexingResponse { get; set; }
    }

    /// <inheritdoc/>
    public class GeocodeAndIndexResumeResponseValue : GeocodeAndIndexResponseValue
    {
        /// <summary>
        /// The resume you sent to be geocoded/indexed with geolocation coordinates added
        /// </summary>
        public ParsedResume ResumeData { get; set; }
    }

    /// <inheritdoc/>
    public class GeocodeAndIndexJobResponseValue : GeocodeAndIndexResponseValue
    {
        /// <summary>
        /// The job you sent to be geocoded/indexed with geolocation coordinates added
        /// </summary>
        public ParsedJob JobData { get; set; }
    }
}

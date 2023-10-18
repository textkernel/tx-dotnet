// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;
using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.Geocoding
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

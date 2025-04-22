// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.Indexes;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;

namespace Textkernel.Tx.Models.API.Geocoding
{
    /// <inheritdoc/>
    public class GeocodeAndIndexResumeRequest : GeocodeAndIndexRequest
    {
        /// <summary>
        /// The resume you wish to be geocoded/indexed
        /// </summary>
        public ParsedResume ResumeData { get; set; }
    }

    /// <inheritdoc/>
    public class GeocodeAndIndexJobRequest : GeocodeAndIndexRequest
    {
        /// <summary>
        /// The job you wish to be geocoded/indexed
        /// </summary>
        public ParsedJob JobData { get; set; }
    }

    /// <summary>
    /// Request body for geocoding a document and then adding into an index
    /// </summary>
    public class GeocodeAndIndexRequest
    {
        /// <summary>
        /// Indicates whether or not the document should still be added to the index if the geocode request fails.
        /// </summary>
        public bool IndexIfGeocodeFails { get; set; }

        /// <summary>
        /// Geocoding settings
        /// </summary>
        public GeocodeOptionsBase GeocodeOptions { get; set; }

        /// <summary>
        /// Where to index the resume
        /// </summary>
        public IndexingOptionsGeneric IndexingOptions { get; set; }
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Indexes;
using Sovren.Models.Job;
using Sovren.Models.Resume;

namespace Sovren.Models.API.Geocoding
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
        public IndexSingleDocumentInfo IndexingOptions { get; set; }
    }
}

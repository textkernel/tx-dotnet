// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Parsing
{
    /// <summary>
    /// Information about a document conversion
    /// </summary>
    public class ConversionMetadata
    {
        /// <summary>
        /// The file type that was detected
        /// </summary>
        public string DetectedType { get; set; }

        /// <summary>
        /// The suggested extension based on the <see cref="DetectedType"/>
        /// </summary>
        public string SuggestedFileExtension { get; set; }

        /// <summary>
        /// The computed validity based on the source text. This will indicate whether
        /// a document looks like a legitimate resume/job or not. See <see href="https://sovren.com/technical-specs/latest/rest-api/resume-parser/overview/document-conversion-code/"/>
        /// </summary>
        public string OutputValidityCode { get; set; }

        /// <summary>
        /// How long the document conversion took, in milliseconds.
        /// This is a subset of <see cref="ApiResponseInfo.TotalElapsedMilliseconds"/>
        /// </summary>
        public int ElapsedMilliseconds { get; set; }

        /// <summary>
        /// The MD5 hash of the document bytes
        /// </summary>
        public string DocumentHash { get; set; }
    }
}

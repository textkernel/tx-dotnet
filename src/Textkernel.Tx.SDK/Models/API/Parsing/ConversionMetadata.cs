// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.Parsing
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
        /// a document looks like a legitimate resume/job or not. See <see href="https://developer.textkernel.com/tx-platform/v10/resume-parser/overview/document-conversion-code/"/>
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

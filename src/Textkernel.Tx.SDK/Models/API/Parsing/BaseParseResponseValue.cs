// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.Geocoding;

namespace Textkernel.Tx.Models.API.Parsing
{
    /// <summary>
    /// The <see cref="ApiResponse{T}.Value"/> from a Parse response
    /// </summary>
    public class BaseParseResponseValue : GeocodeAndIndexResponseValue
    {
        /// <summary>
        /// Information about converting the document to plain text
        /// </summary>
        public ConversionMetadata ConversionMetadata { get; set; }

        /// <summary>
        /// Any additional conversions you requested will be here (eg: PDF or HTML)
        /// </summary>
        public Conversions Conversions { get; set; }

        /// <summary>
        /// Information about the parsing transaction
        /// </summary>
        public ParsingMetadata ParsingMetadata { get; set; }

        /// <summary>
        /// The status of the parse transaction
        /// </summary>
        public ApiResponseInfoLite ParsingResponse { get; set; }

        /// <summary>
        /// If profession normalization was requested, the status of the profession normalization transaction will be output here
        /// </summary>
        public ApiResponseInfoLite ProfessionNormalizationResponse { get; set; }
    }
}

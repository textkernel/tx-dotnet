// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Geocoding;

namespace Sovren.Models.API.Parsing
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
    }
}

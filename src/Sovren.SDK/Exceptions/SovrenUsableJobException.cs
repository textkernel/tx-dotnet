// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API;
using Sovren.Models.API.Parsing;

namespace Sovren
{
    /// <summary>
    /// This exception is thrown when an error happens, but the service was still able to produce a usable Job object (see the <see cref="Response"/> property)
    /// </summary>
    public abstract class SovrenUsableJobException : SovrenException
    {
        /// <summary>
        /// This may or may not be <see langword="null"/> or incomplete depending on what specific error occurred
        /// </summary>
        public ParseJobResponse Response { get; protected set; }

        internal SovrenUsableJobException(RestResponse response, ApiResponseInfoLite errorInfo, string transactionId, ParseJobResponse parseResponse)
            : base(null, response, errorInfo, transactionId)
        {
            Response = parseResponse;
        }
    }

    /// <inheritdoc/>
    public class SovrenGeocodeJobException : SovrenUsableJobException
    {
        internal SovrenGeocodeJobException(RestResponse response, ApiResponseInfoLite errorInfo, string transactionId, ParseJobResponse parseResponse)
            : base(response, errorInfo, transactionId, parseResponse) { }
    }

    /// <inheritdoc/>
    public class SovrenIndexJobException : SovrenUsableJobException
    {
        internal SovrenIndexJobException(RestResponse response, ApiResponseInfoLite errorInfo, string transactionId, ParseJobResponse parseResponse)
            : base(response, errorInfo, transactionId, parseResponse) { }
    }
}

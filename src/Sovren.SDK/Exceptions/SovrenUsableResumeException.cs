// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API;
using Sovren.Models.API.Parsing;

namespace Sovren
{
    /// <summary>
    /// This exception is thrown when an error happens, but the service was still able to produce a usable Resume object (see the <see cref="Response"/> property)
    /// </summary>
    public abstract class SovrenUsableResumeException : SovrenException
    {
        /// <summary>
        /// This may or may not be <see langword="null"/> or incomplete depending on what specific error occurred
        /// </summary>
        public ParseResumeResponse Response { get; protected set; }

        internal SovrenUsableResumeException(RestResponse response, ApiResponseInfoLite errorInfo, string transactionId, ParseResumeResponse parseResponse)
            : base(null, response, errorInfo, transactionId)
        {
            Response = parseResponse;
        }
    }

    /// <inheritdoc/>
    public class SovrenGeocodeResumeException : SovrenUsableResumeException
    {
        internal SovrenGeocodeResumeException(RestResponse response, ApiResponseInfoLite errorInfo, string transactionId, ParseResumeResponse parseResponse)
            : base(response, errorInfo, transactionId, parseResponse) { }
    }

    /// <inheritdoc/>
    public class SovrenIndexResumeException : SovrenUsableResumeException
    {
        internal SovrenIndexResumeException(RestResponse response, ApiResponseInfoLite errorInfo, string transactionId, ParseResumeResponse parseResponse)
            : base(response, errorInfo, transactionId, parseResponse) { }
    }
}

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API;
using Textkernel.Tx.Models.API.Parsing;
using System.Net.Http;

namespace Textkernel.Tx
{
    /// <summary>
    /// This exception is thrown when an error happens, but the service was still able to produce a usable Resume object (see the <see cref="Response"/> property)
    /// </summary>
    public abstract class TxUsableResumeException : TxException
    {
        /// <summary>
        /// This may or may not be <see langword="null"/> or incomplete depending on what specific error occurred
        /// </summary>
        public ParseResumeResponse Response { get; protected set; }

        internal TxUsableResumeException(HttpResponseMessage response, ApiResponseInfoLite errorInfo, string transactionId, ParseResumeResponse parseResponse)
            : base(null, response, errorInfo, transactionId)
        {
            Response = parseResponse;
        }
    }

    /// <inheritdoc/>
    public class TxGeocodeResumeException : TxUsableResumeException
    {
        internal TxGeocodeResumeException(HttpResponseMessage response, ApiResponseInfoLite errorInfo, string transactionId, ParseResumeResponse parseResponse)
            : base(response, errorInfo, transactionId, parseResponse) { }
    }

    /// <inheritdoc/>
    public class TxIndexResumeException : TxUsableResumeException
    {
        internal TxIndexResumeException(HttpResponseMessage response, ApiResponseInfoLite errorInfo, string transactionId, ParseResumeResponse parseResponse)
            : base(response, errorInfo, transactionId, parseResponse) { }
    }

    /// <inheritdoc/>
    public class TxProfessionNormalizationResumeException : TxUsableResumeException
    {
        internal TxProfessionNormalizationResumeException(HttpResponseMessage response, ApiResponseInfoLite errorInfo, string transactionId, ParseResumeResponse parseResponse)
            : base(response, errorInfo, transactionId, parseResponse) { }
    }
}

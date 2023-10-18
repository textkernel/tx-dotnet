// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Textkernel.Tx.Models.API;
using Textkernel.Tx.Models.API.Parsing;
using System.Net.Http;

namespace Textkernel.Tx
{
    /// <summary>
    /// This exception is thrown when an error happens, but the service was still able to produce a usable Job object (see the <see cref="Response"/> property)
    /// </summary>
    public abstract class TxUsableJobException : TxException
    {
        /// <summary>
        /// This may or may not be <see langword="null"/> or incomplete depending on what specific error occurred
        /// </summary>
        public ParseJobResponse Response { get; protected set; }

        internal TxUsableJobException(HttpResponseMessage response, ApiResponseInfoLite errorInfo, string transactionId, ParseJobResponse parseResponse)
            : base(null, response, errorInfo, transactionId)
        {
            Response = parseResponse;
        }
    }

    /// <inheritdoc/>
    public class TxGeocodeJobException : TxUsableJobException
    {
        internal TxGeocodeJobException(HttpResponseMessage response, ApiResponseInfoLite errorInfo, string transactionId, ParseJobResponse parseResponse)
            : base(response, errorInfo, transactionId, parseResponse) { }
    }

    /// <inheritdoc/>
    public class TxIndexJobException : TxUsableJobException
    {
        internal TxIndexJobException(HttpResponseMessage response, ApiResponseInfoLite errorInfo, string transactionId, ParseJobResponse parseResponse)
            : base(response, errorInfo, transactionId, parseResponse) { }
    }

    /// <inheritdoc/>
    public class TxProfessionNormalizationJobException : TxUsableJobException
    {
        internal TxProfessionNormalizationJobException(HttpResponseMessage response, ApiResponseInfoLite errorInfo, string transactionId, ParseJobResponse parseResponse)
            : base(response, errorInfo, transactionId, parseResponse) { }
    }
}

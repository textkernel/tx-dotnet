// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API;
using System;
using System.Net;
using System.Net.Http;

namespace Textkernel.Tx
{
    /// <summary>
    /// The most generic exception thrown by the SDK as a result of an error response from the API
    /// </summary>
    public class TxException : Exception
    {
        /// <summary>
        /// The raw response from the API
        /// </summary>
        public HttpResponseMessage ResponseMessage { get; protected set; }

        /// <summary>
        /// The HTTP Status Code of the response. See <see href="https://developer.textkernel.com/Sovren/v10/overview/#http-status-codes"/>
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; protected set; }

        /// <summary>
        /// The Info.Code of the response. This will indicate what type of error occurred. See <see href="https://developer.textkernel.com/Sovren/v10/overview/#http-status-codes"/>
        /// </summary>
        public string TxErrorCode { get; protected set; }

        /// <summary>
        /// The Id of the transaction, use this when reporting errors to Support
        /// </summary>
        public string TransactionId { get; protected set; }

        /// <summary>
        /// The JSON request body, will only have a value if <see cref="TxClient.ShowFullRequestBodyInExceptions"/> is <see langword="true"/>
        /// </summary>
        public string RequestBody { get; protected set; }

        internal TxException(string requestBody, HttpResponseMessage response, ApiResponseInfoLite errorInfo, string transactionId)
            : base(errorInfo?.Message ?? "Invalid response object from API")
        {
            ResponseMessage = response;
            HttpStatusCode = response?.StatusCode ?? HttpStatusCode.InternalServerError;
            TxErrorCode = errorInfo?.Code ?? "Unknown Error";
            TransactionId = transactionId;
            RequestBody = requestBody;
        }

        internal TxException(string requestBody, HttpResponseMessage response, ApiResponseInfo errorInfo)
            : this(requestBody, response, errorInfo, errorInfo.TransactionId)
        {
        }
    }    
}

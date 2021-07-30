// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API
{
    /// <inheritdoc/>
    public class ApiResponseInfo : ApiResponseInfoLite
    {
        /// <summary>
        /// The id for a specific API transaction. Use this when contacting support@sovren.com about issues.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// How long the transaction took on Sovren's server, in milliseconds.
        /// <br/>If the transaction takes longer to complete on the client side, that extra duration is solely network latency.
        /// </summary>
        public int TotalElapsedMilliseconds { get; set; }

        /// <summary>
        /// The version of the parsing engine
        /// </summary>
        public string EngineVersion { get; set; }

        /// <summary>
        /// The version of the API
        /// </summary>
        public string ApiVersion { get; set; }

        /// <summary>
        /// How many credits the customer was charged for this transaction
        /// </summary>
        public decimal TransactionCost { get; set; }

        /// <summary>
        /// Information about the customer who made the API call
        /// </summary>
        public AccountInfo CustomerDetails { get; set; }
    }
}

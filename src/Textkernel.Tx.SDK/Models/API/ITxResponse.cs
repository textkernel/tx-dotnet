// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Textkernel.Tx.Models.API
{
    /// <summary>
    /// The response body from a Sovren API call
    /// </summary>
    public interface ITxResponse
    {
        /// <summary>
        /// Contains information about the response and the customer
        /// </summary>
        ApiResponseInfo Info { get; set; }
    }
}

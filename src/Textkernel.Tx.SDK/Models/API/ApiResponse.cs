// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API
{
    /// <summary>
    /// The response body from a Tx API call
    /// </summary>
    public class ApiResponse<T> : ITxResponse
    {
        /// <inheritdoc/>
        public ApiResponseInfo Info { get; set; }

        /// <summary>
        /// The data returned based on the request type/content
        /// </summary>
        public T Value { get; set; }
    }  
}

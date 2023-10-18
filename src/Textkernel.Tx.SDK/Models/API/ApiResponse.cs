// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Textkernel.Tx.Models.API
{
    /// <inheritdoc/>
    public class ApiResponse<T> : ISovrenResponse
    {
        /// <inheritdoc/>
        public ApiResponseInfo Info { get; set; }

        /// <summary>
        /// The data returned based on the request type/content
        /// </summary>
        public T Value { get; set; }
    }  
}

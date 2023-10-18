// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.Indexes
{
    /// <inheritdoc/>
    public class IndexDocumentResponse : ApiResponse<object> { }

    /// <inheritdoc/>
    public class IndexMultipleDocumentsResponse : ApiResponse<List<IndexMultipleDocumentsResponseValue>> { }

    /// <summary>
    /// One entry in the <see cref="ApiResponse{T}.Value"/> from a 'index multiple documents' response
    /// </summary>
    public class IndexMultipleDocumentsResponseValue : ApiResponseInfoLite
    {
        /// <summary>
        /// Id of the specific document represented in the response
        /// </summary>
        public string DocumentId { get; set; }
    }
}

﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.MatchV2.Response
{
    /// <summary>
    /// The <see cref="ApiResponse{T}.Value"/> from a 'DeleteCandidates' or 'DeleteJobs' response
    /// </summary>
    public class DeleteDocumentsResponseValue
    {
        /// <summary>
        /// List of documents successfully deleted.
        /// </summary>
        public IEnumerable<string> DeletedDocumentIds { get; set; }
    }

    /// <inheritdoc/>
    public class DeleteDocumentsResponse : ApiResponse<DeleteDocumentsResponseValue> { }
}

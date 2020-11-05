// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Indexes
{
    /// <summary>
    /// A small container for identifying an indexed document
    /// </summary>
    public class IndexedDocumentInfo
    {
        /// <summary>
        /// The id for the index that contains the document (case-insensitive).
        /// </summary>
        public string IndexId { get; set; }

        /// <summary>
        /// The id of the document (case-insensitive)
        /// </summary>
        public string DocumentId { get; set; }
    }
}

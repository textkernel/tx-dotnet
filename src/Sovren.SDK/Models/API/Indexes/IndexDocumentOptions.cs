// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Indexes
{
    /// <summary>
    /// Information for adding a document to an index
    /// </summary>
    public class IndexDocumentOptions
    {
        /// <summary>
        /// The id for the index where the document should be added (case-insensitive).
        /// </summary>
        public string IndexId { get; set; }

        /// <summary>
        /// The id to assign to the new document. This is restricted to alphanumeric with dashes and underscores. 
        /// All values will be converted to lower-case.
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// Create an instance given the index and document ids
        /// </summary>
        /// <param name="indexId">The id for the index where the document should be added (case-insensitive).</param>
        /// <param name="documentId">
        /// The id to assign to the new document. This is restricted to alphanumeric 
        /// with dashes and underscores. All values will be converted to lower-case.
        /// </param>
        public IndexDocumentOptions(string indexId, string documentId)
        {
            IndexId = indexId;
            DocumentId = documentId;
        }
    }
}

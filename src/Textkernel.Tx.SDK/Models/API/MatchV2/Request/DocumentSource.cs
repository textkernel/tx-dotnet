// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    /// <summary>
    /// Defines a document that can be used to generate a search query.
    /// </summary>
    public class DocumentSource
    {
        /// <summary>
        /// Specify what type of document is being passed to the match engine.
        /// </summary>
        public DocumentType Type { get; set; }

        /// <summary>
        /// Id of the document in the index to generate the query from.
        /// </summary>
        public string Id { get; set; }
    }
}

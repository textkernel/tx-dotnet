// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Textkernel.Tx.Services;
using Textkernel.Tx.Models.API.MatchV2.Response;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{

    /// <summary>
    /// Request body for a Match request
    /// </summary>
    public class MatchRequest
    {
        /// <summary>
        /// The target environment
        /// </summary>
        public MatchV2Environment SearchAndMatchEnvironment { get; set; }

        /// <summary>
        /// The options for the Match request
        /// </summary>
        public Options Options { get; set; }

        /// <summary>
        /// The query object that will be combined with the match query to drive the search.
        /// </summary>
        public SearchQuery Query { get; set; }

        /// <summary>
        /// The document to generate the search query from.
        /// </summary>
        public DocumentSource SourceDocument { get; set; }
    }
}

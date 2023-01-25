// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Request
{
    /// <summary>
    /// Request body for a 'GetProfessionsTaxonomy' request
    /// </summary>
    public class GetProfessionsTaxonomyRequest
    {
        /// <summary>
        /// The datatype to return the taxonomy in. Can be either json or csv.
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// The language parameter returns the taxonomy with descriptions only in that specified language. If not specified, descriptions in all languages are returned. Must be specified as one of the supported <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-languages">ISO codes</see>.
        /// </summary>
        public string Language { get; set; }
    }
}

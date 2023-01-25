// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Skills.Request
{
    /// <summary>
    /// Request body for a 'GetSkillsTaxonomy' request
    /// </summary>
    public class GetSkillsTaxonomyRequest
    {
        /// <summary>
        /// The datatype to return the taxonomy in. Can be either json or csv.
        /// </summary>
        public string Format { get; set; }
    }
}

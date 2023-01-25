﻿// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Ontology.Request
{
    /// <summary>
    /// Request body for a 'SuggestSkills' request
    /// </summary>
    public class SuggestSkillsRequest
    {
        /// <summary>
        /// The profession code IDs from the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-taxonomies">Sovren Professions Taxonomy</see> for which the service should return related skills. The list can contain up to 10 profession codes.
        /// </summary>
        public List<int> ProfessionCodeIds { get; set; }
        /// <summary>
        /// The maximum amount of suggested skills returned. If not specified this parameter defaults to 10.
        /// </summary>
        public int Limit { get; set; } = 10;
    }
}

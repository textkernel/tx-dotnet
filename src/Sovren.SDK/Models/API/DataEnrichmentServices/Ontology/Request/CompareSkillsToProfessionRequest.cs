// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Ontology.Request
{
    /// <summary>
    /// Request body for a 'CompareSkillsToProfession' request
    /// </summary>
    public class CompareSkillsToProfessionRequest
    {
        /// <summary>
        /// The skill IDs which should be compared against the given profession. The list can contain up to 50 skills.
        /// </summary>
        public List<string> SkillIds { get;set; }
        /// <summary>
        /// The profession code ID from the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-taxonomies">Sovren Professions Taxonomy</see> to compare the skill set to.
        /// </summary>
        public int ProfessionCodeId { get; set; }
    }
}

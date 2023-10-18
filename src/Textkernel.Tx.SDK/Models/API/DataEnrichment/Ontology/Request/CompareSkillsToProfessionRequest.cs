// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Ontology.Request
{
    /// <summary>
    /// Request body for a 'CompareSkillsToProfession' request
    /// </summary>
    public class CompareSkillsToProfessionRequest
    {
        /// <summary>
        /// The skills which should be compared against the given profession. The list can contain up to 50 skills.
        /// </summary>
        public List<SkillScore> Skills { get;set; }
        /// <summary>
        /// The profession code ID from the <see href="https://developer.textkernel.com/Sovren/v10/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see> to compare the skill set to.
        /// </summary>
        public int ProfessionCodeId { get; set; }
        /// <summary>
        /// The language to use for the returned descriptions.
        /// </summary>
        public string OutputLanguage { get; set; }
    }
}

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Ontology.Request
{
    /// <summary>
    /// Request body for a 'SuggestSkills' request
    /// </summary>
    public class SuggestSkillsFromProfessionsRequest
    {
        /// <summary>
        /// The profession code IDs from the <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see> for which the service should return related skills. The list can contain up to 10 profession codes.
        /// </summary>
        public List<int> ProfessionCodeIds { get; set; }
        /// <summary>
        /// The maximum amount of suggested skills returned. The maximum and default is 10.
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// The language to use for the returned descriptions.
        /// </summary>
        public string OutputLanguage { get; set; }
    }
}

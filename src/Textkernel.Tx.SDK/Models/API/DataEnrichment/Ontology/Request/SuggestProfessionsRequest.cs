// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Ontology.Request
{
    /// <summary>
    /// Request body for a 'SuggestProfessions' request
    /// </summary>
    public class SuggestProfessionsRequest
    {
        /// <summary>
        /// The skill IDs (and optionally, scores) used to return the most relevant professions. The list can contain up to 50 skill IDs.
        /// </summary>
        public List<SkillScore> Skills { get; set; }
        /// <summary>
        /// Flag to enable returning a list of missing skills per suggested profession.
        /// </summary>
        public bool ReturnMissingSkills { get; set; } = false;
        /// <summary>
        /// The maximum amount of professions returned. If not specified this parameter defaults to 10.
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// The language to use for the returned descriptions.
        /// </summary>
        public string OutputLanguage { get; set; }
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.DataEnrichment.Ontology.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichment.Ontology.Request
{
    /// <summary>
    /// Request body for a 'Suggest Skills from Skills' request
    /// </summary>
    public class SuggestSkillsFromSkillsRequest
    {
        /// <summary>
        /// The skill IDs (and optionally, scores) used to return the most relevant professions. The list can contain up to 50 skill IDs.
        /// </summary>
        public List<SkillScore> Skills { get; set; }

        /// <summary>
        /// The maximum amount of professions returned. If not specified this parameter defaults to 10.
        /// </summary>
        public int Limit { get; set; } = 25;

        /// <summary>
        /// The language to use for the returned descriptions.
        /// </summary>
        public string OutputLanguage { get; set; }
    }
}

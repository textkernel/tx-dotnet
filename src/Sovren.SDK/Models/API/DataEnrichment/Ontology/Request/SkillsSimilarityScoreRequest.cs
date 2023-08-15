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
    /// Request body for a 'Skills Similarity Score' request
    /// </summary>
    public class SkillsSimilarityScoreRequest
    {
        /// <summary>
        /// The skill IDs (and optionally, scores) to score against the other set of skills. The list can contain up to 50 skills.
        /// </summary>
        public List<SkillScore> SkillsA { get; set; }

        /// <summary>
        /// The skill IDs (and optionally, scores) to score against the other set of skills. The list can contain up to 50 skills.
        /// </summary>
        public List<SkillScore> SkillsB { get; set; }
    }
}

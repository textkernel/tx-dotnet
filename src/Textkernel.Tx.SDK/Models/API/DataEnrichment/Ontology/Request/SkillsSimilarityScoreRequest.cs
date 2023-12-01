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

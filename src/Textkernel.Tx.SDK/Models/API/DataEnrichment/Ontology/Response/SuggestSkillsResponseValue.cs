// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'SuggestSkills' response.
	/// </summary>
    public class SuggestSkillsResponseValue
    {
        /// <summary>
        /// A list of skills related to the given professions or skills in the request.
        /// </summary>
        public List<SkillScore> SuggestedSkills { get; set; }
    }

    /// <summary>
    /// A skill related to the given profession.
    /// </summary>
    public class SkillScore
    {
        /// <summary>
        /// <strong>In a request:</strong> <br/>
        /// The weight that will be applied to the skill. Must be in the range [0 - 1]. These values are relative, 
        /// so <code>SkillA.Score = 0.2; SkillB.Score = 0.4;</code>is equivalent to<code>SkillA.Score = 0.5; SkillB.Score = 1.0;</code>
        /// <br/><strong>In a response:</strong><br/>
        /// A value from [0 - 1] indicating how relative this skill is to the given skills/professions.
        /// </summary>
        public float Score { get; set; }
        /// <summary>
        /// The ID of the skill in the skills taxonomy.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The description of the skill in the Skills Taxonomy. Will only be returned if OutputLanguage is specified in the request.
        /// This has no effect in a request body.
        /// </summary>
        public string Description { get; set; }
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichment.Ontology.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'SuggestSkills' response.
	/// </summary>
    public class SuggestSkillsResponseValue
    {
        /// <summary>
        /// A list of skills related to the given professions.
        /// </summary>
        public List<SkillScore> SuggestedSkills { get; set; }
    }

    /// <summary>
    /// A skill related to the given profession.
    /// </summary>
    public class SkillScore
    {
        /// <summary>
        /// A value from [0 - 1] indicating how relative this skill is to all of the given professions.
        /// </summary>
        public float Score { get; set; }
        /// <summary>
        /// The ID of the skill in the skills taxonomy.
        /// </summary>
        public string Id { get; set; }
    }
}

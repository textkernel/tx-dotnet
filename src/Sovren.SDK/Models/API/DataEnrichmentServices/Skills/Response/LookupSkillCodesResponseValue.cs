// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.DataEnrichmentServices.Ontology.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Skills.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'LookupSkills' response
	/// </summary>
    public class LookupSkillCodesResponseValue
    {
        /// <summary>
        /// List of skills in from the skills taxonomy.
        /// </summary>
        public List<SkillCode> Skills { get; set; }
    }

    /// <summary>
    /// A skill from the skills taxonomy.
    /// </summary>
    public class SkillCode
    {
        /// <summary>
        /// The ID of the skill in the skills taxonomy.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The skill description in the requested language.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Type of skill. Possible values are Professional, IT, Language, or Soft.
        /// </summary>
        public string Type { get; set; }
    }
}

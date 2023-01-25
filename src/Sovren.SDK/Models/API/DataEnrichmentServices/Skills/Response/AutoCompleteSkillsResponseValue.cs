// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Skills.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'AutocompleteSkills' response
	/// </summary>
    public class AutoCompleteSkillsResponseValue
    {
        /// <summary>
        /// A list of skills based on the given Prefix.
        /// </summary>
        public List<AutoCompleteSkill> Skills { get; set; }
    }

    /// <summary>
    /// A skill based on the given Prefix.
    /// </summary>
    public class AutoCompleteSkill
    {
        /// <summary>
        /// The description of the skill in the requested language.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The ID of the skill in the skills taxonomy.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Type of skill. Possible values are Professional, IT, Language, or Soft.
        /// </summary>
        public string Type { get; set; }
    }
}

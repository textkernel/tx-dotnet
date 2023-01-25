// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Skills.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'NormalizeSkills' response
	/// </summary>
    public class NormalizeSkillsResponseValue
    {
        /// <summary>
        /// A list of skills objects.
        /// </summary>
        public List<NormalizeSkill> Skills { get; set; }
    }

    /// <inheritdoc/>
    public class NormalizeSkill : BaseSkill
    {
        /// <summary>
        /// The raw text that matched to a skill description in the provided language.
        /// </summary>
        public string RawText { get; set; }
    }
}

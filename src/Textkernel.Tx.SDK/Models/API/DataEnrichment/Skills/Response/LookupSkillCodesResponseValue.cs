// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.DataEnrichment.Ontology.Response;
using Sovren.Models.DataEnrichment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichment.Skills.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'LookupSkills' response
	/// </summary>
    public class LookupSkillCodesResponseValue
    {
        /// <summary>
        /// List of skills in from the skills taxonomy.
        /// </summary>
        public List<Skill> Skills { get; set; }
    }
}

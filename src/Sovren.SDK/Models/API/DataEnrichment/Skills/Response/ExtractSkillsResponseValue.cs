﻿// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.DataEnrichment;
using System.Collections.Generic;

namespace Sovren.Models.API.DataEnrichment.Skills.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'ExtractSkills' response
	/// </summary>
    public class ExtractSkillsResponseValue
    {
        /// <summary>
        /// Whether the input text was truncated or not due to length.
        /// </summary>
        public bool Truncated { get; set; }
        /// <summary>
        /// A list of extracted skills.
        /// </summary>
        public List<ExtractedSkill> Skills { get; set; }
    }
}
﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.DataEnrichment;
using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.DataEnrichment.Skills.Response
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

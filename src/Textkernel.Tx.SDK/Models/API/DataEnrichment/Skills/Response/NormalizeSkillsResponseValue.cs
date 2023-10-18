// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.DataEnrichment;
using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.DataEnrichment.Skills.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'NormalizeSkills' response
	/// </summary>
    public class NormalizeSkillsResponseValue
    {
        /// <summary>
        /// A list of skills objects.
        /// </summary>
        public List<NormalizedSkill> Skills { get; set; }
    }
}

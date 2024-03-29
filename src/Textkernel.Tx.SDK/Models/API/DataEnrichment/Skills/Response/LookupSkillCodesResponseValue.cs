﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response;
using Textkernel.Tx.Models.DataEnrichment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.API.DataEnrichment.Skills.Response
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

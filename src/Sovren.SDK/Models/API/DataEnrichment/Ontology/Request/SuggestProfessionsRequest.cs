﻿// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichment.Ontology.Request
{
    /// <summary>
    /// Request body for a 'SuggestProfessions' request
    /// </summary>
    public class SuggestProfessionsRequest
    {
        /// <summary>
        /// The skill IDs used to return the most relevant professions. The list can contain up to 50 skill IDs.
        /// </summary>
        public List<string> SkillIds { get; set; }
        /// <summary>
        /// Flag to enable returning a list of missing skills per suggested profession.
        /// </summary>
        public bool ReturnMissingSkills { get; set; } = false;
        /// <summary>
        /// The maximum amount of professions returned. If not specified this parameter defaults to 10.
        /// </summary>
        public int Limit { get; set; } = 10;
    }
}

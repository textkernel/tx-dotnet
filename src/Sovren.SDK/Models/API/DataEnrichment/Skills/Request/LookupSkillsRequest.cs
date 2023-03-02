// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichment.Skills.Request
{
    /// <summary>
    /// Request body for a 'LookupSkills' request
    /// </summary>
    public class LookupSkillsRequest
    {
        /// <summary>
        /// The IDs of the skills to get details about. A maximum of 100 IDs can be requested.
        /// </summary>
        public List<string> SkillIds { get; set; }
        /// <summary>
        /// The language to use for the output skill descriptions. If not provided, defaults to en. If specified, must be one of the supported <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-languages">ISO codes</see>.
        /// </summary>
        public string OutputLanguage { get; set; } = "en";
    }
}

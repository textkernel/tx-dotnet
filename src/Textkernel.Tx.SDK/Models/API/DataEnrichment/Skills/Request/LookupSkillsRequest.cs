﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Skills.Request
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
        /// The language to use for the output skill descriptions. If not provided, defaults to en. If specified, must be one of the supported <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </summary>
        public string OutputLanguage { get; set; } = "en";
    }
}

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Skills.Request
{
    /// <summary>
    /// Request body for a 'NormalizeSkills' request
    /// </summary>
    public class NormalizeSkillsRequest
    {
        /// <summary>
        /// The list of skills to normalize (up to 50 skills, each skill may not exceed 100 characters).
        /// </summary>
        public List<string> Skills { get; set; }
        /// <summary>
        /// The language of the given skills. Must be one of the supported <see href="https://developer.textkernel.com/Sovren/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// The language to use for the output skill descriptions. If not provided, defaults to the input language. Must be one of the supported <see href="https://developer.textkernel.com/Sovren/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </summary>
        public string OutputLanguage { get; set; }
    }
}

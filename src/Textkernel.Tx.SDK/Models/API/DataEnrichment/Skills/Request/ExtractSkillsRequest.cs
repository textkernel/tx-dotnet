// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Textkernel.Tx.Models.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Skills.Request
{
    /// <summary>
    /// Request body for a 'ExtractSkills' request
    /// </summary>
    public class ExtractSkillsRequest
    {
        /// <summary>
        /// The text to extract skills from. There is a 24,000 character limit.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// The language of the input text. Must be one of the supported <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// A value from [0 - 1] for the minimum confidence threshold for extracted skills. Lower values will return more skills, but also increase the likelihood of ambiguity-related errors. The recommended and default value is 0.5.
        /// </summary>
        public float Threshold { get; set; } = 0.5f;
        /// <summary>
        /// The language to use for the output skill descriptions. If not provided, defaults to the input language. Must be one of the supported <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </summary>
        public string OutputLanguage { get; set; }
    }
}

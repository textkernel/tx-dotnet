// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Ontology.Request
{
    /// <summary>
    /// Request body for a 'Suggest Skills from Skills' request
    /// </summary>
    public class SuggestSkillsFromSkillsRequest
    {
        /// <summary>
        /// The skills for which the service should return related skills. The list can contain up to 50 skills.
        /// </summary>
        public List<SkillScore> Skills { get; set; }

        /// <summary>
        /// The maximum amount of suggested skills returned. If not specified this parameter defaults to 25.
        /// </summary>
        public int Limit { get; set; } = 25;

        /// <summary>
        /// The language to use for the returned descriptions.
        /// </summary>
        public string OutputLanguage { get; set; }

        /// <summary>
        /// If specified, only these types of skills will be returned. The following values are acceptable: Professional, IT, Language, Soft, Certification, All. Only applicable in v2 endpoints.
        /// </summary>
        public List<string> Types { get; set; } = new List<string> { };
    }
}

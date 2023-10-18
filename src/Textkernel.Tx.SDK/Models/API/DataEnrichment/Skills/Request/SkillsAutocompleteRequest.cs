// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Skills.Request
{
    /// <inheritdoc/>
    public class SkillsAutoCompleteRequest : AutocompleteRequest
    {
        /// <summary>
        /// If specified, only these types of skills will be returned. The following values are acceptable: Professional, IT, Language, Soft, All.
        /// </summary>
        public List<string> Types { get; set; } = new List<string> { };
    }
}

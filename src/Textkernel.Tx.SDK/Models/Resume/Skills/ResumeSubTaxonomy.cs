// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Textkernel.Tx.Models.Skills;
using System.Collections.Generic;

namespace Textkernel.Tx.Models.Resume.Skills
{
    /// <inheritdoc/>
    public class ResumeSubTaxonomy : FoundSubTaxonomy
    {
        /// <summary>
        /// The skills from this subtaxonomy that were found
        /// </summary>
        public List<ResumeSkill> Skills { get; set; }
    }
}

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Textkernel.Tx.Models.Skills;
using System.Collections.Generic;

namespace Textkernel.Tx.Models.Job.Skills
{
    /// <inheritdoc/>
    public class JobSubTaxonomy : FoundSubTaxonomy
    {
        /// <summary>
        /// The skills from this subtaxonomy that were found
        /// </summary>
        public List<JobSkill> Skills { get; set; }
    }
}

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Skills;
using System;
using System.Collections.Generic;

namespace Textkernel.Tx.Models.Job.Skills
{
    /// <inheritdoc/>
    public class JobSkill : JobSkillVariation
    {
        /// <summary>
        /// The variations (synonyms) of this skill that were found
        /// </summary>
        public List<JobSkillVariation> Variations { get; set; }
    }

    /// <inheritdoc/>
    public class JobSkillVariation : Skill
    {
        /// <summary>
        /// <see langword="true"/> if this skill was listed as 'required' on the job description
        /// </summary>
        public bool Required { get; set; }
    }
}

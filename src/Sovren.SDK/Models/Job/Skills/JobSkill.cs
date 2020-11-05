// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Skills;
using System.Collections.Generic;

namespace Sovren.Models.Job.Skills
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

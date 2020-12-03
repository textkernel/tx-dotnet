// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Skills;
using System;
using System.Collections.Generic;

namespace Sovren.Models.Resume.Skills
{
    /// <inheritdoc/>
    public class ResumeSkill : ResumeSkillVariation
    {
        /// <summary>
        /// The variations (synonyms) of this skill that were found
        /// </summary>
        public List<ResumeSkillVariation> Variations { get; set; }

        /// <summary>
        /// If this skill has any varitaions, this describes the total months experience of those variations
        /// </summary>
        public SovrenPrimitive<int> ChildrenMonthsExperience { get; set; }

        /// <summary>
        /// If this skill has any varitaions, this describes the most recent date any of the varitaions were used
        /// </summary>
        public SovrenPrimitive<DateTime> ChildrenLastUsed { get; set; }
    }

    /// <inheritdoc/>
    public class ResumeSkillVariation : Skill
    {
        /// <summary>
        /// Describes the amount of experience a candidate has with this skill
        /// </summary>
        public SovrenPrimitive<int> MonthsExperience { get; set; }

        /// <summary>
        /// Describes the date the candidate last used the skill (derived from position dates)
        /// </summary>
        public SovrenPrimitive<DateTime> LastUsed { get; set; }

        /// <summary>
        /// Where the skill was found
        /// </summary>
        public List<SectionIdentifier> FoundIn { get; set; }
    }
}

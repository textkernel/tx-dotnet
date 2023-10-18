// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Textkernel.Tx.Models.Skills;
using System;
using System.Collections.Generic;

namespace Textkernel.Tx.Models.Resume.Skills
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
        public TxPrimitive<int> ChildrenMonthsExperience { get; set; }

        /// <summary>
        /// If this skill has any varitaions, this describes the most recent date any of the varitaions were used
        /// </summary>
        public TxPrimitive<DateTime> ChildrenLastUsed { get; set; }
    }

    /// <inheritdoc/>
    public class ResumeSkillVariation : Skill
    {
        /// <summary>
        /// Describes the amount of experience a candidate has with this skill
        /// </summary>
        public TxPrimitive<int> MonthsExperience { get; set; }

        /// <summary>
        /// Describes the date the candidate last used the skill (derived from position dates)
        /// </summary>
        public TxPrimitive<DateTime> LastUsed { get; set; }

        /// <summary>
        /// Where the skill was found
        /// </summary>
        public List<SectionIdentifier> FoundIn { get; set; }
    }
}

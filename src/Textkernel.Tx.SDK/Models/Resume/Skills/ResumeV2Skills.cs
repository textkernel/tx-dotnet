// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;
using System;
using Textkernel.Tx.Models.API.Parsing;
using Textkernel.Tx.Models.Skills;

namespace Textkernel.Tx.Models.Resume.Skills
{
    /// <summary>
    /// Skills output when <see cref="SkillsSettings.TaxonomyVersion"/> is set to (or defaults to) "V2".
    /// </summary>
    public class ResumeV2Skills
    {
        /// <summary>
        /// Array of skills exactly as found in the plain text of the document.
        /// </summary>
        public List<ResumeRawSkill> Raw { get; set; }

        /// <summary>
        /// Normalized skills output when <see cref="SkillsSettings.TaxonomyVersion"/> is set to (or defaults to) "V2"
        /// and <see cref="SkillsSettings.Normalize"/> is set to <see langword="true"/>.
        /// </summary>
        public List<ResumeNormalizedSkill> Normalized { get; set; }

        /// <summary>
        /// Professions most related to the document. Only output if <see cref="SkillsSettings.TaxonomyVersion"/> is set to 
        /// (or defaults to) "V2" and <see cref="SkillsSettings.Normalize"/> is set to <see langword="true"/>.
        /// </summary>
        public List<ProfessionClass> RelatedProfessionClasses { get; set; }
    }

    /// <summary>
    /// Skill exactly as it was found in the plain text of the document.
    /// </summary>
    public class ResumeRawSkill : RawSkill
    {
        /// <summary>
        /// Describes the amount of experience a candidate has with this skill. <see langword="null"/> if unknown.
        /// </summary>
        public TxPrimitive<int> MonthsExperience { get; set; }

        /// <summary>
        /// Describes the date the candidate last used the skill (derived from position dates). <see langword="null"/> if unknown.
        /// </summary>
        public TxPrimitive<DateTime> LastUsed { get; set; }

        /// <summary>
        /// Array of objects denoting where in the document this skill was located.
        /// </summary>
        public List<SectionIdentifier> FoundIn { get; set; }
    }

    /// <summary>
    /// Normalized skill concept representing one or more raw skills that were extracted.
    /// </summary>
    public class ResumeNormalizedSkill : NormalizedSkill
    {
        /// <summary>
        /// Describes the amount of experience a candidate has with this skill. <see langword="null"/> if unknown.
        /// </summary>
        public TxPrimitive<int> MonthsExperience { get; set; }

        /// <summary>
        /// Describes the date the candidate last used the skill (derived from position dates). <see langword="null"/> if unknown.
        /// </summary>
        public TxPrimitive<DateTime> LastUsed { get; set; }

        /// <summary>
        /// Array of objects denoting where in the document this skill was located.
        /// </summary>
        public List<SectionIdentifier> FoundIn { get; set; }
    }
}

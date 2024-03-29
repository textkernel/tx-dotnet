﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Skills;
using System.Collections.Generic;
using Textkernel.Tx.Models.API.Parsing;

namespace Textkernel.Tx.Models.Job.Skills
{
    /// <summary>
    /// Skills output when <see cref="SkillsSettings.TaxonomyVersion"/> is set to (or defaults to) "V2".
    /// </summary>
    public class JobV2Skills
    {
        /// <summary>
        /// Array of skills exactly as found in the plain text of the document.
        /// </summary>
        public List<JobRawSkill> Raw { get; set; }

        /// <summary>
        /// Normalized skills output when <see cref="SkillsSettings.TaxonomyVersion"/> is set to (or defaults to) "V2"
        /// and <see cref="SkillsSettings.Normalize"/> is set to <see langword="true"/>.
        /// </summary>
        public List<JobNormalizedSkill> Normalized { get; set; }

        /// <summary>
        /// Professions most related to the document. Only output if <see cref="SkillsSettings.TaxonomyVersion"/> is set to 
        /// (or defaults to) "V2" and <see cref="SkillsSettings.Normalize"/> is set to <see langword="true"/>.
        /// </summary>
        public List<ProfessionClass> RelatedProfessionClasses { get; set; }
    }

    /// <summary>
    /// Skill exactly as it was found in the plain text of the document.
    /// </summary>
    public class JobRawSkill : RawSkill
    {
        /// <summary>
        /// <see langword="true"/> if this skill was listed as 'required' on the job description
        /// </summary>
        public bool Required { get; set; }
    }

    /// <summary>
    /// Normalized skill concept representing one or more raw skills that were extracted.
    /// </summary>
    public class JobNormalizedSkill : NormalizedSkill
    {
        /// <summary>
        /// <see langword="true"/> if this skill was listed as 'required' on the job description
        /// </summary>
        public bool Required { get; set; }
    }
}

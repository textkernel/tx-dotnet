// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;
using System;

namespace Sovren.Models.Resume.Skills
{

    /// <summary>
    /// Skills output when version 2 of the taxonomy is utilized.
    /// </summary>
    public class SkillsOutput
    {
        /// <summary>
        /// Array of skills exactly as found in the plain text of the document.
        /// </summary>
        public List<RawSkill> Raw { get; set; }

        /// <summary>
        /// Normalized skills output when version 2 of the taxonomy is utilized and SkillsSettings.Normalize is set to true.
        /// </summary>
        public List<NormalizedSkill> Normalized { get; set; }

        /// <summary>
        /// Professions most related to the document. Only output if version 2 of the skills taxonomy is utilized and SkillsSettings.Normalize is set to true.
        /// </summary>
        public List<ProfessionClass> RelatedProfessionClasses { get; set; }
    }

    /// <summary>
    /// Skill exactly as it was found in the plain text of the document.
    /// </summary>
    public class RawSkill
    {
        /// <summary>
        /// The exact skill text extracted from the document.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Describes the amount of experience a candidate has with this skill. Null if unknown.
        /// </summary>
        public SovrenPrimitive<int> MonthsExperience { get; set; }

        /// <summary>
        /// Describes the date the candidate last used the skill (derived from position dates). Null if unknown.
        /// </summary>
        public SovrenPrimitive<DateTime> LastUsed { get; set; }

        /// <summary>
        /// Array of objects denoting where in the document this skill was located.
        /// </summary>
        public List<FoundIn> FoundIn { get; set; }
    }

    /// <summary>
    /// Object denoting a location in the document.
    /// </summary>
    public class FoundIn
    {
        /// <summary>
        /// The section(s) where the skill was found.
        /// </summary>
        public string SectionType {get;set;}

        /// <summary>
        /// If applicable, the Position ID or Education Id.
        /// </summary>
        public string Id {get;set;}
    }

    /// <summary>
    /// Normalized skill concept representing one or more raw skills that were extracted.
    /// </summary>
    public class NormalizedSkill
    {
        /// <summary>
        /// Name of the normalized skill.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of this skill in the skills taxonomy.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Type of skill. Possible values are Professional, IT, or Soft.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Describes the amount of experience a candidate has with this skill. Null if unknown.
        /// </summary>
        public SovrenPrimitive<int> MonthsExperience { get; set; }

        /// <summary>
        /// Describes the date the candidate last used the skill (derived from position dates). Null if unknown.
        /// </summary>
        public SovrenPrimitive<DateTime> LastUsed { get; set; }

        /// <summary>
        /// Array of objects denoting where in the document this skill was located.
        /// </summary>
        public List<FoundIn> FoundIn { get; set; }

        /// <summary>
        /// Array of raw skills that were extracted that normalized to this skill.
        /// </summary>
        public List<string> RawSkills { get; set; } = new List<string>();
    }


    /// <summary>
    /// Profession Class that describes a percentage of the source document.
    /// </summary>
    public class ProfessionClass 
    {
        /// <summary>
        /// Name of the related profession.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of the related profession.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Percent of overall document that relates to this profession.
        /// </summary>
        public double Percent { get; set; }

        /// <summary>
        /// Array of objects representing groups of professions.
        /// </summary>
        public List<ProfessionGroup> Groups { get; set; }

    }

    /// <summary>
    /// Profession Group that describes a percentage of the Profession Class.
    /// </summary>
    public class ProfessionGroup 
    {
        /// <summary>
        /// Name of the profession group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of the profession group.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Precent of parent described by this profession group. All values under a parent will add up to 100%.
        /// </summary>
        public double Percent { get; set; }

        /// <summary>
        /// Array of normalized skills associated to this profession group.
        /// </summary>
        public List<string> NormalizedSkills { get; set; }
    }
}

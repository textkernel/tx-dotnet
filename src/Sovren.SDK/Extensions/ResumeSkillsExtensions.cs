// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Resume.Skills;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sovren
{
    /// <summary></summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class ResumeSkillsExtensions
    {
        /// <summary>
        /// Gets a list of taxonomies and their respective 'PercentOfOverall' value which represents how
        /// concentrated the candidate's skills were in any given taxonomy/industry
        /// </summary>
        public static List<KeyValuePair<string, int>> GetTaxonomiesPercentages(this ParseResumeResponseExtensions exts)
        {
            Dictionary<string, int> taxos = new Dictionary<string, int>();
            List<ResumeTaxonomyRoot> roots = exts.Response.Value.ResumeData?.SkillsData;

            if (roots != null && roots.Count > 0)
            {
                foreach (ResumeTaxonomyRoot root in roots.Where(r => r.Taxonomies != null))
                {
                    foreach (ResumeTaxonomy taxo in root.Taxonomies)
                    {
                        taxos.Add(taxo.Name, taxo.PercentOfOverall);
                    }
                }
            }

            return taxos.OrderBy(kvp => kvp.Value).ToList();
        }

        /// <summary>
        /// Gets a list of subtaxonomies and their respective 'PercentOfOverall' value which represents how
        /// concentrated the candidate's skills were in any given subtaxonomy/specialty
        /// </summary>
        public static List<KeyValuePair<string, int>> GetSubtaxonomiesPercentages(this ParseResumeResponseExtensions exts)
        {
            Dictionary<string, int> taxos = new Dictionary<string, int>();
            List<ResumeTaxonomyRoot> roots = exts.Response.Value.ResumeData?.SkillsData;

            if (roots != null && roots.Count > 0)
            {
                foreach (ResumeTaxonomyRoot root in roots.Where(r => r.Taxonomies != null))
                {
                    foreach (ResumeTaxonomy taxo in root.Taxonomies.Where(t => t.SubTaxonomies != null))
                    {
                        foreach (ResumeSubTaxonomy subtax in taxo.SubTaxonomies)
                        {
                            taxos.Add(subtax.SubTaxonomyName, subtax.PercentOfOverall);
                        }
                    }
                }
            }

            return taxos.OrderBy(kvp => kvp.Value).ToList();
        }

        /// <summary>
        /// Gets a flat list of skills that this candidate has. For more detailed data, use 
        /// the tree-like <see cref="Models.Resume.ParsedResume.SkillsData"/> property in 
        /// the <see cref="Models.API.Parsing.ParseResumeResponseValue.ResumeData"/>
        /// </summary>
        public static IEnumerable<string> GetSkillNames(this ParseResumeResponseExtensions exts)
        {
            List<ResumeTaxonomyRoot> roots = exts.Response.Value.ResumeData?.SkillsData;
            List<string> skills = new List<string>();

            if (roots != null && roots.Count > 0)
            {
                foreach (ResumeTaxonomyRoot root in roots.Where(r => r.Taxonomies != null))
                {
                    foreach (ResumeTaxonomy taxo in root.Taxonomies.Where(t => t.SubTaxonomies != null))
                    {
                        foreach (ResumeSubTaxonomy subtax in taxo.SubTaxonomies.Where(st => st.Skills != null))
                        {
                            foreach (ResumeSkill skill in subtax.Skills)
                            {
                                skills.Add(skill.Name);

                                if (skill.Variations != null && skill.Variations.Count > 0)
                                {
                                    skills.AddRange(skill.Variations.Select(s => s.Name));
                                }
                            }
                        }
                    }
                }
            }

            return skills;
        }

        /// <summary>
        /// Gets a flat list of skills that this candidate has experience with in the last X amount of time.
        /// For more detailed data, use the tree-like <see cref="Models.Resume.ParsedResume.SkillsData"/> 
        /// property in the <see cref="Models.API.Parsing.ParseResumeResponseValue.ResumeData"/>
        /// </summary>
        /// <param name="exts"></param>
        /// <param name="usedSince">The skill must have been used in a job after this date to be returned</param>
        public static IEnumerable<string> GetRecentSkills(this ParseResumeResponseExtensions exts, DateTime usedSince)
        {
            List<ResumeTaxonomyRoot> roots = exts.Response.Value.ResumeData?.SkillsData;
            List<string> skills = new List<string>();

            if (roots != null && roots.Count > 0)
            {
                foreach (ResumeTaxonomyRoot root in roots.Where(r => r.Taxonomies != null))
                {
                    foreach (ResumeTaxonomy taxo in root.Taxonomies.Where(t => t.SubTaxonomies != null))
                    {
                        foreach (ResumeSubTaxonomy subtax in taxo.SubTaxonomies.Where(st => st.Skills != null))
                        {
                            foreach (ResumeSkill skill in subtax.Skills)
                            {
                                if (skill.LastUsed != null && skill.LastUsed.Value >= usedSince)
                                {
                                    skills.Add(skill.Name);
                                }

                                if (skill.Variations != null && skill.Variations.Count > 0)
                                {
                                    foreach (ResumeSkillVariation variation in skill.Variations)
                                    {
                                        if (variation.LastUsed != null && variation.LastUsed.Value >= usedSince)
                                        {
                                            skills.Add(variation.Name);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return skills;
        }

        /// <summary>
        /// Gets a flat list of skills that this candidate has experience with and the associated amount of experience (in months).
        /// For more detailed data, use the tree-like <see cref="Models.Resume.ParsedResume.SkillsData"/> 
        /// property in the <see cref="Models.API.Parsing.ParseResumeResponseValue.ResumeData"/>
        /// </summary>
        public static IEnumerable<KeyValuePair<string, int>> GetSkillsAndMonthsExperience(this ParseResumeResponseExtensions exts)
        {
            List<ResumeTaxonomyRoot> roots = exts.Response.Value.ResumeData?.SkillsData;
            List<KeyValuePair<string, int>> skills = new List<KeyValuePair<string, int>>();

            if (roots != null && roots.Count > 0)
            {
                foreach (ResumeTaxonomyRoot root in roots.Where(r => r.Taxonomies != null))
                {
                    foreach (ResumeTaxonomy taxo in root.Taxonomies.Where(t => t.SubTaxonomies != null))
                    {
                        foreach (ResumeSubTaxonomy subtax in taxo.SubTaxonomies.Where(st => st.Skills != null))
                        {
                            foreach (ResumeSkill skill in subtax.Skills)
                            {
                                if (skill.MonthsExperience != null)
                                {
                                    skills.Add(new KeyValuePair<string, int>(skill.Name, skill.MonthsExperience.Value));
                                }

                                if (skill.Variations != null && skill.Variations.Count > 0)
                                {
                                    foreach (ResumeSkillVariation variation in skill.Variations)
                                    {
                                        if (variation.MonthsExperience != null)
                                        {
                                            skills.Add(new KeyValuePair<string, int>(variation.Name, variation.MonthsExperience.Value));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return skills;
        }
    }
}

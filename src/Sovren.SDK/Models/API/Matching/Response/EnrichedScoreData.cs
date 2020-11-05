// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Matching.Response
{
    /// <summary>
    /// Contains metadata/evidence about scores for a specific match/score result
    /// </summary>
    public class EnrichedScoreData
    {
        /// <summary>
        /// Detailed match information for the Languages category.
        /// </summary>
        public SimpleCategoryScoreData Languages { get; set; }

        /// <summary>
        /// Detailed match information for the Certifications category.
        /// </summary>
        public SimpleCategoryScoreData Certifications { get; set; }

        /// <summary>
        /// Detailed match information for the ExecutiveType category.
        /// </summary>
        public SimpleCategoryScoreData ExecutiveType { get; set; }

        /// <summary>
        /// Detailed match information for the Education category.
        /// </summary>
        public EducationScoreData Education { get; set; }

        /// <summary>
        /// Detailed match information for the Taxonomies category.
        /// </summary>
        public TaxonomiesScoreData Taxonomies { get; set; }

        /// <summary>
        /// Detailed match information for the JobTitles category.
        /// </summary>
        public JobTitlesScoreData JobTitles { get; set; }

        /// <summary>
        /// Detailed match information for the Skills category.
        /// </summary>
        public SkillsScoreData Skills { get; set; }

        /// <summary>
        /// Detailed match information for the ManagementLevel category.
        /// </summary>
        public ManagementLevelScoreData ManagementLevel { get; set; }
    }
}

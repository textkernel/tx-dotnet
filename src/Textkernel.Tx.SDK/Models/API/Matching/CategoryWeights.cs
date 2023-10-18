// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Matching
{
    /// <summary>
    /// Weights for each category used in matching/scoring
    /// </summary>
    public class CategoryWeights
    {
        /// <summary>
        /// The weight of the Education category relative to other categories.
        /// </summary>
        public double Education { get; set; }

        /// <summary>
        /// The weight of the JobTitles category relative to other categories.
        /// </summary>
        public double JobTitles { get; set; }

        /// <summary>
        /// The weight of the Skills category relative to other categories.
        /// </summary>
        public double Skills { get; set; }

        /// <summary>
        /// The weight of the Industries/Taxonomies category relative to other categories.
        /// </summary>
        public double Industries { get; set; }

        /// <summary>
        /// The weight of the Languages category relative to other categories.
        /// </summary>
        public double Languages { get; set; }

        /// <summary>
        /// The weight of the Certifications category relative to other categories.
        /// </summary>
        public double Certifications { get; set; }

        /// <summary>
        /// The weight of the ExecutiveType category relative to other categories.
        /// </summary>
        public double ExecutiveType { get; set; }

        /// <summary>
        /// The weight of the ManagementLevel category relative to other categories.
        /// </summary>
        public double ManagementLevel { get; set; }

        /// <summary>
        /// If <see langword="false"/>, the Education category has no data and should be ignored/hidden.
        /// </summary>
        public bool EducationHasData { get; set; }

        /// <summary>
        /// If <see langword="false"/>, the JobTitles category has no data and should be ignored/hidden.
        /// </summary>
        public bool JobTitlesHasData { get; set; }

        /// <summary>
        /// If <see langword="false"/>, the Skills category has no data and should be ignored/hidden.
        /// </summary>
        public bool SkillsHasData { get; set; }

        /// <summary>
        /// If <see langword="false"/>, the Industries/Taxonomies category has no data and should be ignored/hidden.
        /// </summary>
        public bool IndustriesHasData { get; set; }

        /// <summary>
        /// If <see langword="false"/>, the Languages category has no data and should be ignored/hidden.
        /// </summary>
        public bool LanguagesHasData { get; set; }

        /// <summary>
        /// If <see langword="false"/>, the Certifications category has no data and should be ignored/hidden.
        /// </summary>
        public bool CertificationsHasData { get; set; }

        /// <summary>
        /// If <see langword="false"/>, the ExecutiveType category has no data and should be ignored/hidden.
        /// </summary>
        public bool ExecutiveTypeHasData { get; set; }

        /// <summary>
        /// If <see langword="false"/>, the ManagementLevel category has no data and should be ignored/hidden.
        /// </summary>
        public bool ManagementLevelHasData { get; set; }
    }
}

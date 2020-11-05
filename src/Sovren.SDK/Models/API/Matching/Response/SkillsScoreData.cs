// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.API.Matching.Response
{
    /// <inheritdoc/>
    public class SkillsScoreData : CategoryScoreData
    {
        /// <summary>
        /// List of terms requested, but not found.
        /// </summary>
        public List<string> NotFound { get; set; }

        /// <summary>
        /// List of skills found in both source and target documents
        /// </summary>
        public List<FoundSkill> Found { get; set; }
    }

    /// <summary>
    /// Information about a skill match
    /// </summary>
    public class FoundSkill
    {
        /// <summary>
        /// Name of the skill found.
        /// </summary>
        public string Skill { get; set; }

        /// <summary>
        /// <see langword="true"/> when the skill is found in the current time-frame.
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}

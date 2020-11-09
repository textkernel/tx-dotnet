// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Utilities;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.Matching.Request
{
    /// <summary>
    /// Amount of experience obtained/required for a skill
    /// </summary>
    public enum SkillExperienceLevel
    {
        /// <summary>
        /// 0-3 years
        /// </summary>
        Low,

        /// <summary>
        /// 4-6 years
        /// </summary>
        Mid,

        /// <summary>
        /// 7+ years
        /// </summary>
        High
    }

    /// <summary>
    /// Filter for a specific skill
    /// </summary>
    public class SkillFilter
    {
        /// <summary>
        /// The name of the skill. Supports (*, ?) wildcard characters after the third character in the term
        /// as defined in <see href="https://docs.sovren.com/Documentation/AIMatching#ai-filtering-special-operators"/>
        /// </summary>
        public string SkillName { get; set; }

        /// <summary>
        /// The experience level of the skill
        /// </summary>
        [JsonConverter(typeof(StringNullableEnumConverter<SkillExperienceLevel?>))]
        public SkillExperienceLevel? ExperienceLevel { get; set; }

        /// <summary>
        /// Whether or not the skill must be a current skill
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}

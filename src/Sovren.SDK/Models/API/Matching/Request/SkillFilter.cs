// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Text.Json.Serialization;

namespace Sovren.Models.API.Matching.Request
{
    /// <summary>
    /// Amount of experience obtained/required for a skill
    /// </summary>
    public enum SkillExperienceLevel
    {
        /// <summary>
        /// Unknown/not set
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 0-3 years
        /// </summary>
        Low = 1,

        /// <summary>
        /// 4-6 years
        /// </summary>
        Mid = 2,

        /// <summary>
        /// 7+ years
        /// </summary>
        High = 3
    }

    /// <summary>
    /// Filter for a specific skill
    /// </summary>
    public class SkillFilter
    {
        /// <summary>
        /// The name of the skill. Supports (*, ?) wildcard characters after the third character in the term
        /// as defined in <see href="https://sovren.com/technical-specs/latest/rest-api/ai-matching/overview/querying/#ai-filtering-special-operators"/>
        /// </summary>
        public string SkillName { get; set; }

        /// <summary>
        /// The experience level of the skill
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SkillExperienceLevel ExperienceLevel { get; set; }

        /// <summary>
        /// Whether or not the skill must be a current skill
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}

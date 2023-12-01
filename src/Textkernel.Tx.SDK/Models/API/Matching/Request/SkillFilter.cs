// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.API.Matching.Request
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
        /// as defined in <see href="https://developer.textkernel.com/tx-platform/v10/ai-matching/overview/querying/#ai-filtering-special-operators"/>
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
